using System;
using System.Collections.Generic;
using System.IO;

namespace TileBeautify {
    internal class Shortcut {
        //当前用户开始菜单程序文件夹
        public static readonly string UserStartMenu = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
        //所有用户开始菜单程序文件夹
        private static readonly string CommonStartMenu = Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms);
        //开始菜单快捷方式路径list
        private readonly List<string> lnkPathList = new List<string>();

        //递归获取文件夹中文件以及子文件夹中的文件
        private void GetLnkPathList(string folder) {
            var d = new DirectoryInfo(folder);

            foreach (var fi in d.GetFiles()) {
                //不读取隐藏文件
                if (fi.Attributes != FileAttributes.Hidden) {
                    string lnkPath = fi.FullName;
                    string format = Path.GetExtension(lnkPath).ToLower();
                    if (format == ".lnk") {
                        lnkPathList.Add(lnkPath);
                    }
                }
            }

            foreach (var di in d.GetDirectories()) {
                GetLnkPathList(di.FullName);
            }
        }

        //是否已存在快捷方式
        private bool IsAlreadyHasLnk(string exePath, out List<string> pathList) {
            GetLnkPathList(UserStartMenu);
            GetLnkPathList(CommonStartMenu);
            var wsh = new IWshRuntimeLibrary.WshShell();
            bool isAlreadyHasLnk = false;
            pathList = new List<string>();
            foreach (string item in lnkPathList) {
                var shortcut = (IWshRuntimeLibrary.IWshShortcut)wsh.CreateShortcut(item);
                string targetPath = shortcut.TargetPath;
                //考虑快捷方式带参数，故不直接判断相等
                bool flag = targetPath.Contains(exePath);
                if (flag) {
                    isAlreadyHasLnk = true;
                    pathList.Add(item);
                }
            }
            return isAlreadyHasLnk;
        }

        //创建或重建快捷方式
        public void CreateShortcut(string exePath, string tileName, string iconPath, int iconIndex, bool isUrl) {
            var wsh = new IWshRuntimeLibrary.WshShell();
            bool isAlreadyHasLnk = IsAlreadyHasLnk(exePath, out var lnkPathList);

            if (isAlreadyHasLnk) {
                foreach (var lnkPath in lnkPathList) {
                    var shortcut = (IWshRuntimeLibrary.IWshShortcut)wsh.CreateShortcut(lnkPath);
                    string newLnkPath = Path.GetDirectoryName(lnkPath) + "\\" + tileName + ".lnk";
                    string targetPath = shortcut.TargetPath;             //目标位置
                    string workingDirectory = shortcut.WorkingDirectory; //工作目录
                    string description = shortcut.Description;           //备注
                    string hotkey = shortcut.Hotkey;                     //快捷键
                    int windowStyle = shortcut.WindowStyle;              //运行方式
                    File.Delete(lnkPath);

                    shortcut = (IWshRuntimeLibrary.IWshShortcut)wsh.CreateShortcut(newLnkPath);
                    shortcut.TargetPath = targetPath;
                    shortcut.WorkingDirectory = workingDirectory;
                    shortcut.Description = description;
                    shortcut.Hotkey = hotkey;
                    shortcut.WindowStyle = windowStyle;
                    shortcut.IconLocation = iconPath + "," + iconIndex.ToString();
                    shortcut.Save();
                }
            }
            else {
                string folder;
                if (isUrl == true) {
                    folder = FormEditConfig.StartMenuUrl;
                }
                else {
                    folder = FormEditConfig.StartMenuExe;
                }
                Directory.CreateDirectory(folder);
                string newLnkPath = folder + tileName + ".lnk";
                var shortcut = (IWshRuntimeLibrary.IWshShortcut)wsh.CreateShortcut(newLnkPath);
                shortcut.TargetPath = exePath;
                shortcut.WorkingDirectory = Path.GetDirectoryName(exePath);
                shortcut.WindowStyle = 1;
                shortcut.IconLocation = iconPath + "," + iconIndex.ToString();
                shortcut.Save();
            }
        }
    }
}
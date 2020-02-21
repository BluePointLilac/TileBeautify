using System.Diagnostics;

namespace TileBeautify {
    class SystemHidenFile {
        public static void HideFile(string filePath) {
            //文件属性设为系统级隐藏
            Process process = new Process();
            var startInfo = new ProcessStartInfo {
                FileName = "cmd.exe",
                /// cmd参数
                /// /C为执行后立即退出
                /// +h 隐藏文件属性
                /// +s 系统文件属性
                Arguments = "/C" + "attrib +h +s " + filePath,
                //不显示黑窗口
                CreateNoWindow = true,
            };
            process.StartInfo = startInfo;
            process.Start();
            process.Close();
        }
    }
}

using Android.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DownloadHelper
    {
        #region 版本更新
        public static async Task<int> CreateDownloadTask(string filePath,string urlToDownload, IProgress<DownloadBytesProgress> progessReporter)
        {
            int receivedBytes = 0;
            int totalBytes = 0;
            WebClient client = new WebClient();
            using (var stream = await client.OpenReadTaskAsync(urlToDownload))
            {
                using (FileStream fileSeam = new FileStream(filePath, FileMode.Create))
                {
                    byte[] buffer = new byte[4096];
                    totalBytes = Int32.Parse(client.ResponseHeaders[HttpResponseHeader.ContentLength]);

                    for (int i = receivedBytes; i < totalBytes; i = receivedBytes)
                    {
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead == 0)
                        {
                            await Task.Yield();
                            break;
                        }
                        Task task=fileSeam.WriteAsync(buffer, 0, bytesRead);
                        
                        receivedBytes += bytesRead;
                        if (progessReporter != null)
                        {
                            DownloadBytesProgress args = new DownloadBytesProgress(urlToDownload, receivedBytes, totalBytes);
                            progessReporter.Report(args);
                        }
                    }
                }
            }
            return receivedBytes;
        }

        /// <summary>
        /// 下载的文件进行安装
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="context"></param>
        public static void installApk(string filePath, ContextWrapper context)
        {

            if (context == null)
                return;
            // 通过Intent安装APK文件
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(Android.Net.Uri.Parse("file://" + filePath), "application/vnd.android.package-archive");
            //Uri content_url = Uri.Parse(filePath);
            //intent.SetData(content_url);
            intent.SetFlags(ActivityFlags.NewTask);
            context.StartActivity(intent);
        }
        /// <summary>
        /// 这个方法的第一件事就是获取要下载的文件的大小。
        /// 然后使用async / await API在后台线程中一次下载4096个字节。 
        /// 在每个4096字节块之后，UI将被更新以显示下载的进度。 下载完成后，我们将返回收到的字节数。
        /// </summary>
        public class DownloadBytesProgress
        {
            public DownloadBytesProgress(string fileName, int bytesReceived, int totalBytes)
            {
                Filename = fileName;
                BytesReceived = bytesReceived;
                TotalBytes = totalBytes;
            }

            public int TotalBytes { get; private set; }

            public int BytesReceived { get; private set; }

            public float PercentComplete { get { return (float)BytesReceived / TotalBytes; } }

            public string Filename { get; private set; }

            public bool IsFinished { get { return BytesReceived == TotalBytes; } }
        }
        #endregion
    }
}

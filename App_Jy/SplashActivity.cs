using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Content.PM;
using Common;
using System.Threading.Tasks;
using static Common.DownloadHelper;
using System.Threading;

namespace App_Jy
{
    [Activity(Label = "集运App", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        private SplashActivity mContext;
        private TextView tv_version_code;
        private TextView tv_progress;
        private EditText ed_enter_sp;
        private EditText ed_port_sp;
        private int name;
        private String url_down;
        private ProgressBar _progressBar;
        private System.Threading.Timer _timer;
        private TimerCallback timerDelegate;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Splash);
            IntiView();
        }

       
        /// <summary>
        /// 初始化界面UI
        /// </summary>
        private void IntiView()
        {
            tv_version_code = FindViewById<TextView>(Resource.Id.tv_version_code);
            tv_progress = FindViewById<TextView>(Resource.Id.tv_progress);
            _progressBar = FindViewById<ProgressBar>(Resource.Id.progBar);
            var VerName = new AndroidCommon.BaseActivity().GetVersionName(PackageName, PackageManager);
            tv_version_code.Text = VerName;
            CheckVersonAsync();
            Log.Debug("versionname", VerName);
        }

        /// <summary>
        /// 比对版本号进行更新
        /// </summary>
        private async void CheckVersonAsync()
        {
            try
            {
                string url = "http://47.90.48.6:8888/pda/GetVosionNo";
                IDictionary<string, string> dictionary = new Dictionary<string, string>(); 
                var result = await Common.HttpHelp.SendGetHttpRequestBaseOnHttpWebRequest(url, dictionary);
                var data = Common.DataWorking.JsonToObject<Models.Version>(result);
                if (data.EJEAndroidPDA.Count > 0)
                {
                    if (tv_version_code.Text == data.EJEAndroidPDA[0].Vosoin)
                    {
                        url_down = data.EJEAndroidPDA[0].Geturl;
                        StartDownloadHandler();
                        timerDelegate = new TimerCallback(Tick); //tick为执行防范  
                        _timer = new System.Threading.Timer(timerDelegate, null, 0, 1000);

                    }
                    else
                    {
                        EnterHome();
                    }
                }
            }
            catch (PackageManager.NameNotFoundException e)
            {
                Toast.MakeText(this, e.Message, ToastLength.Short);
            }
            

        }
        /// <summary>
        /// 更新界面Ui下载值
        /// </summary>
        /// <param name="state"></param>
        private void Tick(object state)
        {
            this.RunOnUiThread(() =>
            {
                if (tv_progress.Visibility == ViewStates.Gone)
                {
                    tv_progress.Visibility = ViewStates.Visible;
                }
                tv_progress.Text = "下载中：" + _progressBar.Progress;
                if (_progressBar.Progress == 100)
                {
                    timerDelegate.Clone();
                    _timer.Dispose();
                    string tarPath = Android.OS.Environment.ExternalStorageDirectory + "/JYandroidpda.apk";
                    Common.DownloadHelper.installApk(tarPath, this);
                }
            });
            
        }
        /// <summary>
        /// 执行下载
        /// </summary>
        public async void StartDownloadHandler()
        {
            _progressBar.Progress = 0;
            Progress<DownloadBytesProgress> progressReporter = new Progress<DownloadBytesProgress>();
            progressReporter.ProgressChanged += (s, args) =>_progressBar.Progress = (int)(100 * args.PercentComplete);
          
            string tarPath = Android.OS.Environment.ExternalStorageDirectory+ "/JYandroidpda.apk";
            Task<int> downloadTask = DownloadHelper.CreateDownloadTask(tarPath, url_down, progressReporter);
            int bytesDownloaded = await downloadTask;
            System.Diagnostics.Debug.WriteLine("Downloaded {0} bytes.", bytesDownloaded);
        }


        /// <summary>
        /// 跳转登录页
        /// </summary>
        private void EnterHome()
        {
            Intent intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
            Finish();
        }
    }

}
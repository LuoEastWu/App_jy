using System;


using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using static Android.Widget.TextView;

namespace App_Jy.AndroidCommon
{
    [Activity(Label = "BaseActivity")]
    public class BaseActivity : Activity
    {
        /// <summary>
        /// androidmanifest中获取版本名称
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public String GetVersionName(string strPackage, PackageManager manager)
        {
            try
            {
                PackageInfo packageInfo = manager.GetPackageInfo(strPackage, 0);
                int versionCode = packageInfo.VersionCode;
                String versionName = packageInfo.VersionName;
                Console.WriteLine("versionName=" + versionName + ";versionCode=" + versionCode);
                return versionName;
            }
            catch (PackageManager.NameNotFoundException e)
            {
                e.PrintStackTrace();
            }
            return "";
        }


        public static void EdtiTextView(View  v)
        {
            v.FindFocus();
            v.FocusableInTouchMode = true;
            v.RequestFocus();
            v.FindFocus();
        }

        public class SetOnEditorActionListener : TextView, IOnEditorActionListener
        {
            public Context _context;
            public SetOnEditorActionListener(Context context) : base(context)
            {
                _context = context;
            }

            public bool OnEditorAction(TextView v, [GeneratedEnum] ImeAction actionId, KeyEvent e)
            {
                if (string.IsNullOrWhiteSpace(v.Text))
                {
                    Toast.MakeText(_context, "请您输入登录账号！", ToastLength.Short).Show();
                    AndroidCommon.MyMediaPlayer.GetInstance(_context).PyayerMedia();
                    return false;
                }
                return true;
            }
        }


    }
}
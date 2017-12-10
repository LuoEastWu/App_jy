using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Views.InputMethods;
using Android.Media;
using static Android.Media.MediaPlayer;
using static Android.Widget.TextView;

namespace App_Jy
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        private TextView tv_version;
        private EditText ed_login_num;
        private EditText ed_login_pass;
        private String result;
        private ProgressDialog p;
        private String result_mhdata;
        private Button btn_login;
        private String result_mhdata1;
        private Bitmap bitmap_logo;
        private Button btn_set;
        private ImageView img_net;
        private MainActivity mContext;
        private String http;
        private ProgressDialog pDialog;

        private String enter;
        private String port;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Login);
            InitView();
        }

        private void InitView()
        {
            // TODO Auto-generated method stub
            tv_version = FindViewById<TextView>(Resource.Id.tv_version);
            ed_login_num = FindViewById<EditText>(Resource.Id.ed_login_num);
            ed_login_pass = FindViewById<EditText>(Resource.Id.ed_login_pass);
            btn_login = FindViewById<Button>(Resource.Id.btn_login);
            btn_set = FindViewById<Button>(Resource.Id.btn_setting);
            tv_version.Text = "版本号：" + new AndroidCommon.BaseActivity().GetVersionName(PackageName, PackageManager);
            btn_login.Click += Btn_login_Click;
            btn_set.Click += Btn_set_Click;


            ed_login_num.EditorAction += Ed_login_num_EditorAction;
            
            ed_login_pass.EditorAction += Ed_login_pass_EditorAction;
            
        }

      

        private void Ed_login_pass_EditorAction(object sender, EditorActionEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ed_login_pass.Text))
            {
                Toast.MakeText(this, "请您输入登录密码！", ToastLength.Short).Show();
                AndroidCommon.MyMediaPlayer.GetInstance(this).PyayerMedia();
                AndroidCommon.BaseActivity.EdtiTextView(ed_login_pass);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(ed_login_num.Text) && !string.IsNullOrWhiteSpace(ed_login_pass.Text))
                {
                    
                    AndroidCommon.BaseActivity.EdtiTextView(btn_login);
                }
                else
                {
                    AndroidCommon.BaseActivity.EdtiTextView(ed_login_num);
                }
                
            }
        }

        

   
        private void Ed_login_num_EditorAction(object sender, EditorActionEventArgs e)
        {
            if (e.ActionId == ImeAction.Next)
            {
                if (string.IsNullOrWhiteSpace(ed_login_num.Text))
                {
                    Toast.MakeText(this, "请您输入登录账号！", ToastLength.Short).Show();
                    AndroidCommon.MyMediaPlayer.GetInstance(this).PyayerMedia();
                }
                else
                {
                    AndroidCommon.BaseActivity.EdtiTextView(ed_login_pass);
                }
            }
        }







        private void Btn_set_Click(object sender, EventArgs e)
        {

        }

        private void Btn_login_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "开始登录吧！", ToastLength.Short).Show();
        }


    }
}
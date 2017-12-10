using Android.App;
using Android.Content;

using Android.Media;

using static Android.Media.MediaPlayer;

namespace App_Jy.AndroidCommon
{
    public class MyMediaPlayer: Activity
    {
        private Android.Media.MediaPlayer _mediaPlayer;
        private static MyMediaPlayer player;
        private Android.Content.Context context;

        private MyMediaPlayer(Android.Content.Context context)
        {
            this.context = context;
        }
        public static MyMediaPlayer GetInstance(Android.Content.Context MyContext)
        {
            if (player == null)
            {
                player = new MyMediaPlayer(MyContext);
            }
            return player;
        }
       
        public virtual void PyayerMedia()
        {
            _mediaPlayer = Android.Media.MediaPlayer.Create(context,Resource.Drawable.error);
            _mediaPlayer.Start();
            //IOnCompletionListener onCompletion=new IMediaPlayerCompletion();
            //onCompletion.OnCompletion(_mediaPlayer);
            //_mediaPlayer.SetOnCompletionListener(onCompletion);
        }

       

        public class IMediaPlayerCompletion : MediaPlayer,IOnCompletionListener
        {
            public void OnCompletion(MediaPlayer mp)
            {
                mp.Release();
            }
        }
    }
}

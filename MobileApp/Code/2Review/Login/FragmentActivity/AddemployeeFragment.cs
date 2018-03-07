using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Login.Activity;

namespace Login.FragmentActivity
{
    public class AddemployeeFragment : Fragment
    {
        public AddemployeeFragment()
        {

        }
        public static Fragment newInstance(Context context)
        {
            AddemployeeFragment busrouteFragment = new AddemployeeFragment();
            return busrouteFragment;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewGroup root = (ViewGroup)inflater.Inflate(Resource.Layout.Addemployee, null);
                      // Create your application here

            Button Form = root.FindViewById<Button>(Resource.Id.Form);
            // MyButton = (Button)FindViewById(Resource.Id.MyButton);


            Intent i;


            Form.Click += delegate
            {
                i = new Intent(root.Context, typeof(newuser));
                StartActivity(i);
            };

            Button MyButton = root.FindViewById<Button>(Resource.Id.MyButton);
            MyButton.Click += delegate
            {
                LayoutInflater LayoutInflaterAndroid = LayoutInflater.From(this.Context);
                View mView = LayoutInflaterAndroid.Inflate(Resource.Layout.userinput, null);
                Android.Support.V7.App.AlertDialog.Builder alertdialogbuilder = new Android.Support.V7.App.AlertDialog.Builder(this.Context);
                alertdialogbuilder.SetView(mView);


                alertdialogbuilder.SetCancelable(false)
                .SetPositiveButton("SENT", delegate
                {
                    Toast.MakeText(this.Context, "upload file", ToastLength.Short).Show();
                })


                .SetNegativeButton("cancle", delegate
                {
                    alertdialogbuilder.Dispose();

                });
                Android.Support.V7.App.AlertDialog alertDialogandroid = alertdialogbuilder.Create();
                alertDialogandroid.Show();
            };
            return root;
        }
           
        }
    }

﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using System;
using Microsoft.WindowsAzure.MobileServices;
using static Android.Resource;

namespace AzureDemo
{
    [Activity(Label = "AzureDemo", Theme = "@android:style/Theme.DeviceDefault.NoActionBar", MainLauncher = true)]
    public class MainActivity : Activity
    {

        RecyclerView recyclerView;
        AdapterDesignation mAdapterDesignation;
        AzureDataService dataService;
        List<Candidate> Datalist;
        Android.App.ProgressDialog progress,progress1;
        //Button nameDelete;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var tool = FindViewById<Android.Widget.Toolbar>(Resource.Id.toolbar);
            SetActionBar(tool);

            CurrentPlatform.Init();
            UIReference();
            SetAdapter();
        }

        private void UIReference()
        {
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerview);
            dataService = new AzureDataService();
            dataService.Initialize();
        }

        public async void Delete(Candidate Id)
        {
            if (Datalist != null && Datalist.Count != 0)
            {
                await dataService.DeleteDesignation(Id);
                SetAdapter();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Top_Menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            View view = LayoutInflater.Inflate(Resource.Layout.AddDesig, null);
            AlertDialog builder = new AlertDialog.Builder(this).Create();
            builder.SetView(view);
            builder.SetCanceledOnTouchOutside(false);

            Button buttoncancel = view.FindViewById<Button>(Resource.Id.btnCancel);
            buttoncancel.Click += delegate
            {

                builder.Dismiss();
                Toast.MakeText(this, "Alert dialog dismissed!", ToastLength.Short).Show();
            };

            builder.Show();

            Button buttonadd = view.FindViewById<Button>(Resource.Id.btnAdd);
            buttonadd.Click += async delegate
            {
                EditText adcat = view.FindViewById<EditText>(Resource.Id.adcat);

               string userValue = adcat.Text.ToString();
                if (userValue == "")
                {
                    Toast.MakeText(this, "Field Cannot Be Left Blank", ToastLength.Short).Show();
                }
                else
                {
                    progress = new Android.App.ProgressDialog(this);
                    progress.Indeterminate = true;
                    progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                    progress.SetMessage("Adding Please wait...");
                    progress.SetCancelable(false);
                    progress.Show();
                    await dataService.AddDesignation(userValue);
                    progress.Dismiss();
                    SetAdapter();
                    StartActivity(typeof(MainActivity));
                }

            };

            return base.OnOptionsItemSelected(item);
        }

        private async void SetAdapter()
        {
            try
            {
                Datalist = new List<Candidate>();
                //progress1 = new Android.App.ProgressDialog(this);
                //progress1.Indeterminate = true;
                //progress1.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                //progress1.SetMessage("Loading...");
                //progress1.SetCancelable(false);
                //progress1.Show();
                Datalist = await dataService.GetDesignation();
                //progress1.Dismiss();

                if (mAdapterDesignation == null)
                {
                    mAdapterDesignation = new AdapterDesignation(this, Datalist);

                    LinearLayoutManager manager =
                        new LinearLayoutManager(this);

                    recyclerView.SetLayoutManager(manager);
                    recyclerView.SetAdapter(mAdapterDesignation);
                }
                else
                {
                    mAdapterDesignation.mDataList = Datalist;
                    mAdapterDesignation.NotifyDataSetChanged();
                }
            }
            catch (Exception e)
            {
            }
            finally
            {  
            }
        }
    }
}


using Android.App;
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
    [Activity(Label = "ListQuestion", Theme = "@android:style/Theme.DeviceDefault.NoActionBar", MainLauncher = true)]
    public class ListQuestion :Activity
    {

        RecyclerView QuestionRecyclerView;
        AdapterQuestion mAdapterQuestion;
        AzureDataService dataService;
        List<Question> Datalist;
        Android.App.ProgressDialog progress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ListQuestion);

            var tool = FindViewById<Android.Widget.Toolbar>(Resource.Id.Questiontoolbar);
            SetActionBar(tool);

            CurrentPlatform.Init();
            UIReference();
            SetAdapter();
        }

        private void UIReference()
        {
            QuestionRecyclerView = FindViewById<RecyclerView>(Resource.Id.QuestionRecyclerview);
            dataService = new AzureDataService();
            dataService.Initialize();
        }

        public async void Delete(Question Id)
        {
            if (Datalist != null && Datalist.Count != 0)
            {
                progress = new Android.App.ProgressDialog(this);
                progress.Indeterminate = true;
                progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                progress.SetMessage("Deleting...");
                progress.SetCancelable(false);
                progress.Show();
                await dataService.DeleteQuestion(Id);
                progress.Dismiss();
                SetAdapter();
            }
        }

        public void Update()
        {
            StartActivity(typeof(UpdateDesignation));
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Top_Menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            View view = LayoutInflater.Inflate(Resource.Layout.AddQuestion, null);
            AlertDialog builder = new AlertDialog.Builder(this).Create();
            builder.SetView(view);
            builder.SetCanceledOnTouchOutside(false);

            Button QuebtnCancel = view.FindViewById<Button>(Resource.Id.QuebtnCancel);
            QuebtnCancel.Click += delegate
            {
                builder.Dismiss();
                Toast.MakeText(this, "Alert dialog dismissed!", ToastLength.Short).Show();
            };

            builder.Show();

            Button QuebtnAdd = view.FindViewById<Button>(Resource.Id.QuebtnAdd);
            QuebtnAdd.Click += async delegate
            {
                EditText adque = view.FindViewById<EditText>(Resource.Id.adque);

                string Que = adque.Text.ToString();
                if (Que == "")
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
                    await dataService.AddQuestion(Que);
                    progress.Dismiss();
                    SetAdapter();
                }

            };

            return base.OnOptionsItemSelected(item);
        }
        private async void SetAdapter()
        {
            try
            {
                Datalist = new List<Question>();
                progress = new Android.App.ProgressDialog(this);
                progress.Indeterminate = true;
                progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                progress.SetMessage("Loading...");
                progress.SetCancelable(false);
                progress.Show();
                Datalist = await dataService.GetQuestion();
                progress.Dismiss();

                if (mAdapterQuestion == null)
                {
                    mAdapterQuestion = new AdapterQuestion(this, Datalist);

                    LinearLayoutManager manager =
                        new LinearLayoutManager(this);

                    QuestionRecyclerView.SetLayoutManager(manager);
                    QuestionRecyclerView.SetAdapter(mAdapterQuestion);
                }
                else
                {
                    mAdapterQuestion.mDataList = Datalist;
                    mAdapterQuestion.NotifyDataSetChanged();
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
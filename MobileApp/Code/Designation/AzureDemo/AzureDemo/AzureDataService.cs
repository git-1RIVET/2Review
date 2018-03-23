using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace AzureDemo
{
    class AzureDataService
    {

        public MobileServiceClient MobileService { get; set; }

        IMobileServiceSyncTable<Designation> DesigTable;
        IMobileServiceSyncTable<Question> QuesTable;

        public async Task Initialize()
        {
            //Get our sync table that will call out to azure
            MobileService = new MobileServiceClient("https://2review.azurewebsites.net");

            DesigTable = MobileService.GetSyncTable<Designation>();
            QuesTable = MobileService.GetSyncTable<Question>();

            //setup our local sqlite store and intialize our table
            const string path = "2review.db";
            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<Designation>();
            store.DefineTable<Question>();

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
        }

        public async Task<List<Designation>> GetDesignation()
        {
            await SyncDesignation();
            var result = await DesigTable.OrderByDescending(D => D.DesignationName).ToListAsync();

            return result;
        }

        public async Task<List<Designation>> GetUpdateDesig(Designation Id)
        {
            await SyncDesignation();
            var result = await DesigTable.Where(D => D.Id.Equals(Id)).ToListAsync();

            return result;
        }

        public async Task<List<Question>> GetQuestion()
        {
            await SyncDesignation();
            var result = await QuesTable.OrderByDescending(Q => Q.Question_Text).ToListAsync();
            return result;
        }

        public async Task<bool> DeleteDesignation(Designation id)
        {
            await DesigTable.DeleteAsync(id);
            await SyncDesignation();
            return true;
        }

        public async Task<bool> DeleteQuestion(Question id)
        {
            await QuesTable.DeleteAsync(id);
            await SyncDesignation();
            return true;
        }

        public async Task AddDesignation(string DesignationName)
        {
            var review = new Designation
            {
                DesignationName = DesignationName
            }; 

            await DesigTable.InsertAsync(review);
            await SyncDesignation();
        }

        public async Task AddQuestion(string QuestionText)
        {
            var review = new Question
            {
                Question_Text = QuestionText
            };

            await QuesTable.InsertAsync(review);
            await SyncDesignation();
        }



        public async Task SyncDesignation()
        {
            await DesigTable.PullAsync("allDesignation", DesigTable.CreateQuery());
            await QuesTable.PullAsync("allQuestion", QuesTable.CreateQuery());
            await MobileService.SyncContext.PushAsync();
        }
    }
}
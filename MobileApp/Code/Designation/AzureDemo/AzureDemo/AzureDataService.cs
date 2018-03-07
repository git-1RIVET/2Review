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

        IMobileServiceSyncTable<Candidate> DesigTable;

        public async Task Initialize()
        {
            //Get our sync table that will call out to azure
            MobileService = new MobileServiceClient("https://2review.azurewebsites.net");

            DesigTable = MobileService.GetSyncTable<Candidate>();

            //setup our local sqlite store and intialize our table
            const string path = "2review.db";
            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<Candidate>();

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
        }

        public async Task<List<Candidate>> GetDesignation()
        {
            await SyncDesignation();
            var result = await DesigTable.OrderByDescending(D => D.title).ToListAsync();

            return result;
        }

        public async Task<bool> DeleteDesignation(Candidate id)
        {
            await DesigTable.DeleteAsync(id);
            await SyncDesignation();
            return true;
        }

        public async Task AddDesignation(string DesignationName)
        {
            var review = new Candidate
            {
                title = DesignationName
            }; 

            await DesigTable.InsertAsync(review);
            await SyncDesignation();
        }

        public async Task SyncDesignation()
        {
            await DesigTable.PullAsync("allDesignation", DesigTable.CreateQuery());
            await MobileService.SyncContext.PushAsync();
        }

    }
}
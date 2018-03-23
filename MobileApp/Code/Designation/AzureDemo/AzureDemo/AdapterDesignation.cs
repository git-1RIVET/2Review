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
using Android.Support.V7.Widget;
using System.Threading.Tasks;

namespace AzureDemo
{
    class AdapterDesignation : RecyclerView.Adapter
    {
        Context context;
    public List<Designation> mDataList;

    public AdapterDesignation(Context context, List<Designation> mData)
    {
        this.context = context;
        this.mDataList = mData;
    }
        public override int ItemCount => mDataList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var dataitem = mDataList[position];
            var viewholder = holder as AdapterDesignationViewHolder;
            if (dataitem != null)
            {
                viewholder.designation.Text = dataitem.DesignationName.ToString();
            }

            viewholder.nameDelete.Click += delegate
            {
                ((MainActivity)context).Delete(dataitem);
            };

            viewholder.nameEdit.Click += delegate
            {
                ((MainActivity)context).Update();
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var id = Resource.Layout.Row_Item;
            var itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
            AdapterDesignationViewHolder madapterrecyclerviewholder = new AdapterDesignationViewHolder(itemView);
            return madapterrecyclerviewholder;
        }
    }

    class AdapterDesignationViewHolder : RecyclerView.ViewHolder
    {
        public TextView designation;
        public Button nameDelete;
        public Button nameEdit;
        public AdapterDesignationViewHolder(View itemview) : base(itemview)
        {

            designation = itemview.FindViewById<TextView>(Resource.Id.designation);
            nameDelete = itemview.FindViewById<Button>(Resource.Id.nameDelete);
            nameEdit = itemview.FindViewById<Button>(Resource.Id.nameEdit);
        }
    }
}
    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace AzureDemo
{
    class AdapterDesig : RecyclerView.Adapter
    {

        Context context;
        public List<Student> mDataList;
        public AdapterDesig(Context context, List<Student> mData)
        {
            this.context = context;
            this.mDataList = mData;
        }

        public override int ItemCount => mDataList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var dataitem = mDataList[position];
            var viewholder = holder as AdapterDesigViewHolder;
            if (dataitem != null)
            {
                viewholder.mStud.Text = dataitem.name.ToString();
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var id = Resource.Layout.Row_Item;
            var itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
            AdapterDesigViewHolder madapterrecyclerviewholder = new AdapterDesigViewHolder(itemView);
            return madapterrecyclerviewholder;
        }
    }

    class AdapterDesigViewHolder : RecyclerView.ViewHolder
    {
        public TextView mStud;

        public AdapterDesigViewHolder(View itemview) : base(itemview)
        {
            mStud = itemview.FindViewById<TextView>(Resource.Id.textviewMadetathome);
        }
    }

}

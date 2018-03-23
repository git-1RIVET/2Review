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
    class AdapterQuestion : RecyclerView.Adapter
    {
        Context context;
        public List<Question> mDataList;

        public AdapterQuestion(Context context, List<Question> mData)
        {
            this.context = context;
            this.mDataList = mData;
        }
        public override int ItemCount => mDataList.Count;
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var dataitem = mDataList[position];
            var viewholder = holder as AdapterQuestionViewHolder;
            if (dataitem != null)
            {
                viewholder.Question.Text = dataitem.Question_Text.ToString();
            }

            viewholder.QuestionDelete.Click += delegate
            {
                ((ListQuestion)context).Delete(dataitem);
            };

            viewholder.QuestionEdit.Click += delegate
            {
                ((ListQuestion)context).Update();
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var id = Resource.Layout.Question_Row_Item;
            var itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
            AdapterQuestionViewHolder madapterrecyclerviewholder = new AdapterQuestionViewHolder(itemView);
            return madapterrecyclerviewholder;
        }
    }

    class AdapterQuestionViewHolder : RecyclerView.ViewHolder
    {
        public TextView Question;
        public Button QuestionDelete;
        public Button QuestionEdit;
        public AdapterQuestionViewHolder(View itemview) : base(itemview)
        {

            Question = itemview.FindViewById<TextView>(Resource.Id.question);
            QuestionDelete = itemview.FindViewById<Button>(Resource.Id.QuestionDelete);
            QuestionEdit = itemview.FindViewById<Button>(Resource.Id.QuestionEdit);
        }
    }
}

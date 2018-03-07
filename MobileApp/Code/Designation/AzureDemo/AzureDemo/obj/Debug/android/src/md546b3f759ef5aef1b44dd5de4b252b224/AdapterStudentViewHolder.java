package md546b3f759ef5aef1b44dd5de4b252b224;


public class AdapterStudentViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("AzureDemo.AdapterStudentViewHolder, AzureDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AdapterStudentViewHolder.class, __md_methods);
	}


	public AdapterStudentViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == AdapterStudentViewHolder.class)
			mono.android.TypeManager.Activate ("AzureDemo.AdapterStudentViewHolder, AzureDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

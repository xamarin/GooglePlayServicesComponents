using System;
using Android.App;
using Android.Widget;
using System.Collections.Generic;
using Android.Content;
using System.Linq;

namespace NearbySample
{
    public class MyListDialog
    {
        private AlertDialog mDialog;
        private ArrayAdapter<string> mAdapter;
        private Dictionary<string, string> mItemMap;

        public MyListDialog (Context context, AlertDialog.Builder builder, EventHandler<DialogClickEventArgs> clickHandler) 
        {
            mItemMap = new Dictionary<string, string> ();
            mAdapter = new ArrayAdapter<string>(context, Android.Resource.Layout.SelectDialogSingleChoice);

            // Create dialog from builder
            builder.SetAdapter (mAdapter, clickHandler);
            mDialog = builder.Create ();
        }


        /**
     * Add an item to the Dialog's list.
     * @param title the human-readable string that should be used to display the item.
     * @param value a value associated with the item that should not be displayed.
     */
        public void AddItem(String title, String value) 
        {
            mItemMap.Add (title, value);
            mAdapter.Add (title);
        }

        /**
     * Remove an item from the list by its title.
     * @param title the title of the item to remove.
     */
        public void RemoveItemByTitle(String title) 
        {
            mItemMap.Remove (title);
            mAdapter.Remove (title);
        }

        /**
     * Remove an item from the list by its associated value.
     * Note: this is an O(n) operation.
     * @param value the value of the item to remove.
     */
        public void RemoveItemByValue (String value) 
        {
            var kvp = mItemMap.FirstOrDefault (i => i.Value == value);

            var key = kvp.Key;
            mItemMap.Remove (key);
            mAdapter.Remove (key);
            mAdapter.NotifyDataSetChanged ();

        }

        /**
     * Get the title of the item at an index,
     * @param index the index of the item in the list.
     * @return the item's title.
     */
        public string GetItemKey(int index)
        {
            return mAdapter.GetItem(index);
        }

        /**
     * Get the value of an item at an index.
     * @param index the index of the item in the list.
     * @return the item's value.
     */
        public string GetItemValue (int index) 
        {
            return mItemMap.ElementAt (index).Value;
        }

        /**
     * Show the dialog (calls AlertDialog#show).
     */
        public void Show ()
        {
            mDialog.Show ();
        }

        /**
     * Dismiss the dialog if it is showing (calls AlertDialog#dismiss).
     */
        public void Dismiss ()
        {
            if (mDialog.IsShowing)
                mDialog.Dismiss ();
        }
    }
}


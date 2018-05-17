package com.example.masuda.sampletodojava;

import android.app.Activity;
import android.content.Context;
import android.view.ContextMenu;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;

import java.util.List;

public class TodoAdapter extends BaseAdapter
{
    private Activity _activity ;
    private List<ToDo> _items ;


    public TodoAdapter(Activity act, List<ToDo> items )
    {
        _activity = act ;
        _items = items ;
    }

    @Override
    public int getCount() {
        return _items.size();
    }

    @Override
    public Object getItem(int position) {
        return _items.get( position );
    }

    @Override
    public long getItemId(int position) {
        return _items.get( position ).getId();
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View view = convertView ;
        if ( view == null ) {
            view = _activity.getLayoutInflater().inflate(android.R.layout.simple_list_item_2, null );
        }
        ToDo it = _items.get(position);
        view.findViewById(android.R.


        return null;
    }
}

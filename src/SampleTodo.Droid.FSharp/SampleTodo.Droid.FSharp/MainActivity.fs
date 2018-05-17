namespace SampleTodo.Droid.FSharp

open System

open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget
open SampleTodo.Droid.FSharp.Models

type Resources = SampleTodo.Droid.FSharp.Resource

type TodoAdapter(_activity,_items) =
    inherit BaseAdapter<ToDo>()

    let mutable _activity:Activity
    let mutable _items:ToDoFiltableCollection

    member x.Item position =
        _items.i



[<Activity (Label = "SampleTodo.Droid.FSharp", MainLauncher = true, Icon = "@mipmap/icon")>]
type MainActivity () =
    inherit Activity ()

    // let mutable count:int = 1

    override this.OnCreate (bundle) =

        base.OnCreate (bundle)

        // Set our view from the "main" layout resource
        this.SetContentView (Resources.Layout.Main)

        // Get our button from the layout resource, and attach an event to it
        // let button = this.FindViewById<Button>(Resources.Id.myButton)
        // button.Click.Add (fun args -> 
        //    button.Text <- sprintf "%d clicks!" count
        //    count <- count + 1
        // )


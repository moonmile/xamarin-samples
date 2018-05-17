module SampleTodo.Droid.FSharp.Models

open System
open System.ComponentModel
open System.Collections.Generic

type ObservableObject() =
    let propChangedEvent = new DelegateEvent<PropertyChangedEventHandler> ()
    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member x.PropertyChanged = propChangedEvent.Publish
    member x.OnPropertyChanged propertyName =
        propChangedEvent.Trigger [|x; new PropertyChangedEventArgs(propertyName)|]
    member x.SetProperty( backingStore:'a byref, value:'a , propertyName ) =
        if EqualityComparer<'a>.Default.Equals( backingStore, value ) = false then
            backingStore <- value
            x.OnPropertyChanged( propertyName )
        true

/// ToDo のアイテムクラス
type ToDo() =
    inherit ObservableObject()

    let mutable id = 0
    let mutable text = ""
    let mutable dueDate: Option<DateTime> = None
    let mutable completed = false
    let mutable createAt  = DateTime.Now
    // 識別ID
    member x.Id 
        with get() = id
        and set(v) = id <- v
    // 項目名
    member x.Text
        with get() = text
        and set(v) = text <- v
    // 期日
    member x.DueDate
        with get() = dueDate
        and set(v) = 
                x.SetProperty( ref dueDate, v, "DueDate" ) |> ignore
                x.OnPropertyChanged("DispDueDate")
                x.OnPropertyChanged("StrDueDate")
    // 完了フラグ
    member x.Completed 
        with get() = completed
        and set(v) = completed <- v
    // 作成日
    member x.CreateAt 
        with get() = createAt
        and set(v) = createAt <- v
    // 期日の指定の有無
    // 選択型のDatePicker用の変換
    // ラベル用に文字列に変換
    // 編集用のコピーメソッド
    // 初期化メソッド
   
/// 設定クラス
type Setting() =
    let mutable dispCompleted = false
    let mutable sortOrder = 0

    member x.DispCompleted  
        with get() = dispCompleted
        and set(v) = dispCompleted <- v
    member x.SortOrder  
        with get() = sortOrder
        and set(v) = sortOrder <- v



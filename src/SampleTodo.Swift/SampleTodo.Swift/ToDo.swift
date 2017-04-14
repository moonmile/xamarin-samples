//
//  ToDo.swift
//  SampleTodo.Swift
//
//  Created by tomoaki on 2017/04/13.
//  Copyright © 2017年 tomoaki. All rights reserved.
//

import Foundation

class ToDo {
    var Id : Int = 0
    var Text : String = ""
    var DueDate : Date? = nil
    var Completed : Bool = false
    var CreateAt : Date = Date()
    init( id: Int, text: String, dueDate: Date?, createAt: Date ) {
        self.Id = id
        self.Text = text
        self.DueDate = dueDate
        self.CreateAt = createAt
    }
    func StrDuDate() -> String {
        if self.DueDate == nil {
            return ""
        } else {
            let fmt = DateFormatter()
            fmt.dateFormat = "yyyy-MM-dd"
            return fmt.string(from: self.DueDate! )
        }
    }
}

class Setting {
    var DispCompleted : Bool = true
    var SortOrder : Int = 0
}


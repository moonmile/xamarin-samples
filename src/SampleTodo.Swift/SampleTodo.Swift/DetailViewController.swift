//
//  DetailViewController.swift
//  SampleTodo.Swift
//
//  Created by tomoaki on 2017/04/12.
//  Copyright © 2017年 tomoaki. All rights reserved.
//

import UIKit
import Foundation

class DetailViewController: UIViewController {

    // @IBOutlet weak var detailDescriptionLabel: UILabel!
    @IBOutlet weak var labelId: UILabel!
    @IBOutlet weak var textText: UITextField!
    @IBOutlet weak var swDue: UISwitch!
    @IBOutlet weak var textDue: UITextField!
    @IBOutlet weak var swCompleted: UISwitch!
    @IBOutlet weak var labelCreateAt: UILabel!
    @IBOutlet var tapGesture: UITapGestureRecognizer!

    func configureView() {
        // Update the user interface for the detail item.
        if let item = detailItem {
            labelId?.text = item.Id.description
            textText?.text = item.Text
            swDue?.isOn = item.DueDate != nil
            textDue?.text = item.StrDuDate()
            swCompleted?.isOn = item.Completed
            let fmt = DateFormatter()
            fmt.dateFormat = "yyyy-MM-dd hh:mm"
            labelCreateAt?.text = fmt.string(from: item.CreateAt)
        }
        
        // 期日をクリックした時にカレンダー表示
        let pkDue = UIDatePicker()
        pkDue.datePickerMode = UIDatePickerMode.date
        if detailItem?.DueDate == nil {
            pkDue.date = Date()
        } else {
            pkDue.date = (detailItem?.DueDate!)!
        }
        self.textDue?.inputView = pkDue
        
        self.tapGesture.addTarget(self, action: #selector(self.tapView(tap:)))
    }

    func tapView( tap: UITapGestureRecognizer ) {
        self.textDue.resignFirstResponder()
    }

    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        configureView()
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }

    var detailItem: ToDo? {
        didSet {
            // Update the view.
            configureView()
        }
    }
    @IBAction func SwDue_ValueChanged(_ sender: Any) {
        if self.swDue?.isOn == true {
            textDue?.isHidden = false
            if self.detailItem?.DueDate == nil {
                self.detailItem?.DueDate = Date()
            }
            textDue?.text = detailItem?.StrDuDate()
        } else {
            textDue?.isHidden = true
        }
    }
    @IBAction func SwCompleted_ValueChanged(_ sender: Any) {
        //
    }
}


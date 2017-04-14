//
//  MasterViewController.swift
//  SampleTodo.Swift
//
//  Created by tomoaki on 2017/04/12.
//  Copyright © 2017年 tomoaki. All rights reserved.
//

import UIKit
import Foundation

class MasterViewController: UITableViewController {

    var detailViewController: DetailViewController? = nil
    var settingViewController: SettingViewController? = nil
    var appSetting = Setting()
    var items = [ToDo]()


    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        
        let fmt = DateFormatter()
        fmt.dateFormat = "yyyy-MM-dd"
        items.append(ToDo(id: 1, text: "item no.1",
                        dueDate: fmt.date(from: "2017-05-01")!,
                        createAt: fmt.date(from: "2017-03-01")!))
        items.append(ToDo(id: 1, text: "item no.2",
                          dueDate: fmt.date(from: "2017-05-03")!,
                          createAt: fmt.date(from: "2017-03-02")!))
        items.append(ToDo(id: 1, text: "item no.3",
                          dueDate: fmt.date(from: "2017-05-02")!,
                          createAt: fmt.date(from: "2017-03-03")!))
        
    }

    override func viewWillAppear(_ animated: Bool) {
        clearsSelectionOnViewWillAppear = splitViewController!.isCollapsed
        super.viewWillAppear(animated)
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }

    func insertNewObject(_ sender: Any) {
        /*
        objects.insert(NSDate(), at: 0)
        let indexPath = IndexPath(row: 0, section: 0)
        tableView.insertRows(at: [indexPath], with: .automatic)
         */
    }

    // MARK: - Segues

    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if segue.identifier == "showDetail" {
            if let indexPath = tableView.indexPathForSelectedRow {
                let item = items[indexPath.row]
                let controller = segue.destination as! DetailViewController
                controller.detailItem = item
            }
        }
        /*
        if segue.identifier == "showDetail" {
            if let indexPath = tableView.indexPathForSelectedRow {
                let object = objects[indexPath.row] as! NSDate
                let controller = (segue.destination as! UINavigationController).topViewController as! DetailViewController
                controller.detailItem = object
                controller.navigationItem.leftBarButtonItem = splitViewController?.displayModeButtonItem
                controller.navigationItem.leftItemsSupplementBackButton = true
            }
        }
         */
    }

    @IBAction func UnwindToMasterView( segue: UIStoryboardSegue ) {
        if segue.identifier == "unwindFromDetail" {
            
        }
        else if segue.identifier == "unwindFromSetting" {
            
        }
    }
    
    // MARK: - Table View

    override func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }

    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return items.count
    }

    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "Cell", for: indexPath)

        let item = items[indexPath.row]
        cell.textLabel!.text = item.StrDuDate()
        cell.detailTextLabel!.text = item.Text
        return cell
    }

    override func tableView(_ tableView: UITableView, canEditRowAt indexPath: IndexPath) -> Bool {
        // Return false if you do not want the specified item to be editable.
        return true
    }

    override func tableView(_ tableView: UITableView, commit editingStyle: UITableViewCellEditingStyle, forRowAt indexPath: IndexPath) {
        if editingStyle == .delete {
            items.remove(at: indexPath.row)
            tableView.deleteRows(at: [indexPath], with: .fade)
        } else if editingStyle == .insert {
            // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
        }
    }
}


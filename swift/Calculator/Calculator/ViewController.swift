//
//  ViewController.swift
//  Calculator
//
//  Created by Seshu Koorella on 6/13/17.
//  Copyright © 2017 seshuk. All rights reserved.
//

import UIKit

class ViewController: UIViewController {
    
    @IBOutlet weak var display: UILabel!
    
    var userIsInTheMiddleOfTyping = false
    
    @IBAction func digitTouched(_ sender: UIButton) {
        
        let digit = sender.currentTitle!
        
        if userIsInTheMiddleOfTyping {
            let textCurrentlyInDisplay = display.text!
            display.text = textCurrentlyInDisplay + digit
        }
        else{
            display.text = digit
            userIsInTheMiddleOfTyping = true
        }
        
    }
    
    var displayValue: Double {
        get {
            return Double(display.text!)!
        }
        
        set {
             display.text = String(newValue)
             userIsInTheMiddleOfTyping = false
        }
    }
    
    @IBAction func performOperation(_ sender: UIButton) {
        
        if let mathSymbol = sender.currentTitle {
            
            switch mathSymbol {
            case "π":
                displayValue = Double.pi
            case "√":
                displayValue = sqrt(displayValue)
            case "AC":
                displayValue = 0
            default:
                break
            }
           
        }
    }
    
    
}


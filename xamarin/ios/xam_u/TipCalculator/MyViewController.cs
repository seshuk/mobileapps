using System;
using CoreGraphics;
using UIKit;

namespace TipCalculator
{
    public class MyViewController : UIViewController
    {
        public MyViewController()
        {
        }

        public override void ViewDidLoad()
        {
            
            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.Yellow;

            UITextField TotalAmount = new UITextField(new CGRect(28, 28, this.View.Bounds.Width - 40, 35)){
                KeyboardType = UIKeyboardType.NumberPad ,
                BorderStyle = UITextBorderStyle.RoundedRect,
                Placeholder = "Enter an amount"
            };

            UIButton calc = new UIButton(UIButtonType.Custom) { 
                Frame = new CGRect(20, 71, View.Bounds.Width-40, 35 ),
                BackgroundColor = UIColor.FromRGB(0, 0.5f, 0)
            };

            calc.SetTitle("Calculate", UIControlState.Normal);

            UILabel resultLabel = new UILabel(new CGRect(0, 124, View.Bounds.Width, 40)) {
                TextColor = UIColor.Blue,
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.SystemFontOfSize(24),
                Text = "Tip is $0.00"
            };

            View.AddSubviews(TotalAmount, calc, resultLabel);

            calc.TouchUpInside += (s, e) => {
                Double val = 0;
                Double.TryParse(TotalAmount.Text, out val);


                resultLabel.Text = string.Format("Tip is ${0}", val * 0.2);

                TotalAmount.ResignFirstResponder();
            };
        }

    }
}

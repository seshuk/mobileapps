using Android.App;
using Android.Widget;
using Android.OS;

namespace TipCalculator
{
    [Activity(Label = "TipCalculator", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        EditText inputBill;
        Button calcButton;
        TextView outputTip;
        TextView outputTotal;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            inputBill = FindViewById<EditText>(Resource.Id.inputBill);
            calcButton = FindViewById<Button>(Resource.Id.calcButton);
            outputTip = FindViewById<TextView>(Resource.Id.outputTip);
            outputTotal = FindViewById<TextView>(Resource.Id.outputTotal);

            calcButton.Click += (sender, e) => {
                var billAmt = inputBill.Text;
                var bill = double.Parse(billAmt);

                var tip = bill * 0.15;
                var total = bill + tip;

                outputTip.Text = tip.ToString();
                outputTotal.Text = total.ToString();

            };
        }
    }
}


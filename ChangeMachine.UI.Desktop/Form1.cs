using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChangeMachine.Core;
using ChangeMachine.Core.DataContract;

namespace ChangeMachine.UI.Desktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void UxBtnDo_Click(object sender, EventArgs e)
        {
            ChangeMachineManager changeMachineManager = new ChangeMachineManager();
            
            // TODO: Validar entradas
            long inputAmountInCents = long.Parse(UxTextInputAmountInCents.Text);
            long priceAmountInCents = long.Parse(UxTextPriceAmountInCents.Text);

            EvaluateChangeRequest request = new EvaluateChangeRequest(inputAmountInCents, priceAmountInCents);
            EvaluateChangeResponse change = changeMachineManager.EvaluateChange(request);

            if (change.HasError)
            {
                UxListCoinCollection.Items.Clear();

                foreach (Error error in change.ErrorList)
                {
                    UxListCoinCollection.Items.Add(string.Format("ERROR: {0}: {1}", error.Property, error.MensagemError));
                }
            }
            else
            {
                ICollection<long> coinsCollection = change.CoinCollection;

                UxListCoinCollection.Items.Clear();

                List<Coin> queryGroupCoins = coinsCollection.GroupBy(coin => coin)
                    .Select(coin => new Coin(coin.Key, coin.Count()))
                    .OrderByDescending(coin => coin.Amount).ToList();

                foreach (Coin coinGroup in queryGroupCoins)
                {
                    decimal coinValue = ConvertCentsToReal(coinGroup.Amount);
                    UxListCoinCollection.Items.Add(string.Format("{0} x {1}", coinGroup.Count, coinValue.ToString("C")));
                }

                UxListCoinCollection.Items.Add(string.Format("Total change: {0}", ConvertCentsToReal(change.TotalAmountInCents).ToString("C")));
            }
        }

        private decimal ConvertCentsToReal(long amountInCents)
        {
            return amountInCents / (decimal)100;
        }
    }
}

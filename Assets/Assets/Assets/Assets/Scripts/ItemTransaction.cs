using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cashbaazi.App.Helper;

namespace Cashbaazi.App.Items
{
    public class ItemTransaction : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI txt_amount;
        [SerializeField] TextMeshProUGUI txt_wallet;
        [SerializeField] TextMeshProUGUI txt_date;
        [SerializeField] TextMeshProUGUI txt_paymentStatus;
        [SerializeField] TextMeshProUGUI txt_transactionId;
        [SerializeField] TextMeshProUGUI txt_remark;


        public void Init(Responce_Transaction _transaction)
        {
            // txt_amount.text = _transaction.Amount.ToString("Amount :- Rs.");
            txt_amount.text = string.Format("Amount : <color=green>{0}", _transaction.Amount);          
            txt_wallet.text = string.Format("Wallet :- <color=red>{0}", _transaction.Wallet);         
            txt_date.text = string.Format("Date :- {0}", _transaction.Date);
            txt_paymentStatus.text = string.Format("Status :- <color=green> {0}", _transaction.PaymentStatus);         
            txt_transactionId.text = string.Format("TransactionId :- {0}", _transaction.TransactionId);
            txt_remark.text = string.Format("Remarks : <color=orange>{0}", _transaction.Remarks);
           
        }
    }
}
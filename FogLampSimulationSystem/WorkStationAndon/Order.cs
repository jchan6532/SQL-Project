using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkStationAndon
{
    public class Order
    {
        #region Public Properties

        /// <summary>
        /// The order ID of the current order
        /// </summary>
        public int OrderId
        {
            get;
        }

        /// <summary>
        /// The order amount for the current order
        /// </summary>
        public int OrderAmount
        {
            get
            {
                int orderAmount = 0;
                SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["justin"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT order_amount FROM LampOrder WHERE order_id = {OrderId}", sqlConnection);

                sqlConnection.Open();
                object response = cmd.ExecuteScalar();
                sqlConnection.Close();

                if (response != null)
                {
                    orderAmount = Convert.ToInt32(response);
                }
                return orderAmount;
            }
        }

        /// <summary>
        /// The order fulfilled bit (1 or 0) representing if the current order has been fullfilled or not
        /// </summary>
        public int OrderFulfilled
        {
            get
            {
                int orderFulfiled = 0;
                SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["justin"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT order_fulfilled FROM LampOrder WHERE order_id = {OrderId}", sqlConnection);

                sqlConnection.Open();
                object response = cmd.ExecuteScalar();
                sqlConnection.Close();

                if (response != null)
                {
                    orderFulfiled = Convert.ToInt32(response);
                }
                return orderFulfiled;
            }
        }

        /// <summary>
        /// The defect amount for the current order
        /// </summary>
        public int Defects
        {
            get
            {
                int defects = 0;
                SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["justin"].ConnectionString);
                SqlCommand cmd = new SqlCommand($"SELECT defects FROM LampOrder WHERE order_id = {OrderId}", sqlConnection);

                sqlConnection.Open();
                object response = cmd.ExecuteScalar();
                sqlConnection.Close();

                if (response != null)
                {
                    defects = Convert.ToInt32(response);
                }
                return defects;
            }
        }

        /// <summary>
        /// The flag representing if the current order is completed or not
        /// </summary>
        public bool IsComplete => OrderAmount >= OrderFulfilled;

        #endregion

        #region Constructor

        /// <summary>
        /// Parameterized constructor that takes an order ID and instantiates a new order object
        /// </summary>
        /// <param name="orderId">order ID</param>
        public Order(int orderId)
        {
            OrderId = orderId;
        }

        #endregion
    }
}

using System;
using System.Configuration;
using System.Data.SqlClient;

namespace WorkstationSimulator
{
    internal class Order
    {
        public int OrderId { get; }

        public int OrderAmount
        {
            get
            {
                int orderAmount = 0;
                SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
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

        public int OrderFulfilled
        {
            get
            {
                int orderFulfiled = 0;
                SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
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


        public int Defects
        {
            get
            {
                int defects = 0;
                SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
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

        public bool IsComplete => OrderAmount >= OrderFulfilled;

        public Order(int orderId)
        {
            OrderId = orderId;
        }
    }
}

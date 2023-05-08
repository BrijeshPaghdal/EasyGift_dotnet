<?php
	include 'check-seller.php';

    $shop_id = $_SESSION['Shop_Id'];
	$stat=3;
	$tempUrl = $url.'api/EasyGift/Shop/'.$shop_id.'/GetOrders/'.$stat;
	$response = fetchRequest($tempUrl);

	if($response['statusCode']==200 && count($response['result']) > 0)
	{
			$output = "
              <table class='table table-bordered table-hover js-basic-example dataTable' Style='border-top:1px;border-bottom:1px;border-left:1px;border-right:1px;border-top:1px'>
               	<thead>
				<tr>
					<th>#</th>
					<th>Product Name</th>
					<th>Company Name</th>
					<th>Customer Name</th>
					<th>Phone no</th>
					<th>Quantity</th>
					<th>Total Price</th>
					<th>Payment</th>
					<th>Order Date</th>
					<th>image</th>
					<th>Status</th>
				</tr>
				</thead>
				<tbody>";


			$i=1;
			foreach ($response['result'] as $row)
			{
				$cust_id = $row['CustomerId'];
				$prod_id = $row['ProductId'];
				$id = $row['Id'];
				$order_id = $row['OrderId'];
				$prod_name = $row['ProductName'];
				$comp_name=$row['CompanyName'];
				$cust_name = $row['FirstName']." ".$row['LastName'];
				$phone_no = $row['PhoneNo'];
				$quantity = $row['Quantity'];
				$total_price = $row['TotalPrice'];
				$pay_type = $row['PaymentMethod'];
				$order_date = $row['OrderDate'];
				$order_date = new DateTime($order_date);
    			$order_date = $order_date->format('d-m-Y');
				$status=$row['Status'];

				$output .="<tr>
				<td>{$i}</td>
				<td>{$prod_name}</td>
				<td>{$comp_name}</td>
				<td>{$cust_name}</td>
				<td>{$phone_no}</td>
				<td>{$quantity}</td>
				<td>{$total_price}</td>
				<td>{$pay_type}</td>
				<td>{$order_date}</td>";
				$i++;

				$tempUrl = $url.'api/EasyGift/Image?filter=ProductId='.$prod_id;
				$res = fetchRequest($tempUrl);
				if ($res['statusCode'] == 200 && count($res['result'])>0) {
				$image_name = "";
				foreach ($res['result'] as $row1) {
					$image_name = $row1['imageName'];
					break;
				}
				}
				else
				{
				$image_name = "";
				}

				$output .="<td class='table-img'><center><img src='./product-images/{$image_name}' height = '40px' width = '40px'></center></td>";
					if ($status == 0){
						$output .= "<td>
					<center><select  class='form-control custom-select label bg-orange'
							style='border-radius: 12px 12px 12px 12px;
							height: 25px;
							width: 90px;
							font-size : 14px
							align : center'

							name='Order_Status' id = 'Order_Status'>";
						$output .= "<option selected>Panding </option>";
						$output .= "<option value = 'Complete/$id'>Complete</option>";
						$output .= "<option value = 'Reject/$id'>Reject</option>";
						$output .= "</select></center>
						</td>";
					}
              		else if($status == 1){
              			$output .= "<td><center><span class='label bg-green shadow-style'>Complete</span></center></td>";
              		}else if($status == 2){
              			$output .= "<td><center><span class='label bg-red shadow-style'>Rejected</span></center></td>";
              		}



				$output.="
     			</tr>";

			}

				$output .= "</tbody>
				<thead>
                  <tr>
                    <th>#</th>
					<th>Product Name</th>
					<th>Company Name</th>
					<th>Customer Name</th>
					<th>Phone no</th>
					<th>Quantity</th>
					<th>Total Price</th>
					<th>Payment</th>
					<th>Order Date</th>
					<th>Image</th>
					<th>Status</th>
                  </tr>
                </thead>
                </table>";
				echo $output;
		}
		else
		{
			echo "
              <table class='table table-bordered table-hover js-basic-example dataTable' Style='border-top:1px;border-bottom:1px;border-left:1px;border-right:1px;border-top:1px'>
               	<thead>
				<tr>
					<th>#</th>
					<th>Product Name</th>
					<th>Company Name</th>
					<th>Customer Name</th>
					<th>Phone no</th>
					<th>Quantity</th>
					<th>Total Price</th>
					<th>Payment</th>
					<th>Order Date</th>
					<th>image</th>
					<th>Status</th>
				</tr>
				</thead>
				<tbody>
				<tr><td colspan = '15'><center> No Record Found</center> </td></tr>
				</tbody>

				</table>
				";
		}

?>

<script src="assets/js/table.min.js"></script>
<!-- Custom Js -->
<script src="assets/js/pages/ui/dialogs.js"></script>
<script src="assets/js/pages/tables/jquery-datatable.js"></script>

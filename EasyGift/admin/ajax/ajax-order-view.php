<?php
require_once('./check-admin.php');
$shop_id = $_POST['shopid'];
function checkShopid($input)
{
	$vale = preg_match("/^(0|[1-9][0-9]*)$/", $input);
	if (!$vale) {
		return "2";
	}
}
$v = checkShopid($shop_id);
$stat=3;
$tempUrl = $url.'api/EasyGift/Shop/'.$shop_id.'/GetOrders/'.$stat;
$response = fetchRequest($tempUrl);

  
  if ($v != "2") {
    if($response['statusCode']==200 && count($response['result'])>0)
	{
		$output = "
              <table class='table table-bordered table-hover js-basic-example dataTable' Style='border-top:1px;border-bottom:1px;border-left:1px;border-right:1px;border-top:1px'>
               	<thead>
				<tr>
					<th>No</th>
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


		$i = 1;
		foreach ($response['result'] as $row) {
			$cust_id = $row['CustomerId'];
			$prod_id = $row['ProductId'];
			$o_id = $row['Id'];
			$order_id = $row['OrderId'];
			$prod_name = $row['ProductName'];
			$cate_name = $row['CategoryName'];
			$comp_name = $row['CompanyName'];
			$cust_name = $row['FirstName'] . " " . $row['LastName'];
			$phone_no = $row['PhoneNo'];
			$quantity = $row['Quantity'];
			$total_price = $row['TotalPrice'];
			$pay_type = $row['PaymentMethod'];
			$order_date = $row['OrderDate'];
			$order_date = new DateTime($order_date);
          	$order_date = $order_date->format('d-m-Y');
			$status = $row['Status'];

			$output .= "<tr>
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
			

			$output .= "<td class='table-img'><center><img src='../vendor/product-images/{$image_name}' height = '40px' width = '40px'></center></td>";
			if ($status == 0) {
				$output .= "<td><span class='label bg-orange shadow-style'>Pending</span></td>";
			} else if ($status == 1) {
				$output .= "<td><span class='label bg-green shadow-style'>Complete</span></td>";
			} else if ($status == 2) {
				$output .= "<td><span class='label bg-red shadow-style'>Rejected</span></td>";
			}



			$output .= "
     			</tr>";
		}

		$output .= "</tbody>
				<thead>
                  <tr>
                    <th>No</th>
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
		//echo "<input type = 'hidden' id = 'cust_id' value = ". $cust_id .">";
		//echo "<input type = 'hidden' id = 'prod_id' value = ". $prod_id .">";
	} else {
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
				<tr><td colspan = '10'><center> No Record Found</center> </td></tr>
				</tbody>

				</table>
				";
	}
?><script src="assets/js/table.min.js"></script>
	<!-- Custom Js -->
	<script src="assets/js/pages/ui/dialogs.js"></script>
	<script src="assets/js/pages/tables/jquery-datatable.js"></script>
<?php } else {
	echo "2";
}
?>
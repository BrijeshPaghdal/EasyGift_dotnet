<?php
	include 'check-seller.php';

    $shop_id = $_SESSION['Shop_Id'];
		$status = $_POST['status'];
		$tempUrl = $url.'api/EasyGift/Shop/'.$shop_id.'/GetOrders/'.$status;
		$response = fetchRequest($tempUrl);

		if($response['statusCode']==200 && count($response['result']) > 0)
		{
			$output = "
              <table class='display table table-hover table-checkable order-column m-t-20 width-per-100' id='tableExport'>
               	<thead>
				<tr>
					<th>#</th>
					<th>Product Name</th>
					<th>Customer Name</th>
					<th>Quantity</th>
					<th>Total Price</th>
					<th>Order Date </th>
					<th class='center'>Status</th>
				</tr>
				</thead>
				<tbody>";


			$i=1;
			foreach ($response['result'] as $row)
			{

				$cust_id = $row['CustomerId'];
				$prod_id = $row['ProductId'];
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
				<td>$i</td>
				<td>{$prod_name}</td>
				<td>{$cust_name}</td>
				<td>{$quantity}</td>
				<td>{$total_price}</td>
				<td>{$order_date}</td>";
					if ($status == 0){
						$output .= "<td><center><span class='label bg-orange shadow-style'>Panding</span></center></td>";
					}
      		else if($status == 1){
      			$output .= "<td><center><span class='label bg-green shadow-style'>Complete</span></center></td>";
      		}else if($status == 2){
      			$output .= "<td><center><span class='label bg-red shadow-style' >Rejected</span></center></td>";
      		}



				$output.="</tr>";
				$i++;
			}

				$output .= "</tbody>
                </table>";
				echo $output;
		}
		else
		{
			echo "
			<table id='quick-product-table' class='table table-hover dashboard-task-infos'>
				<thead>
				<tr align = 'center'>
					<th>#</th>
					<th>Product Name</th>
					<th>Customer Name</th>
					<th>Quantity</th>
					<th>Total Price</th>
					<th>Order Date</th>
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

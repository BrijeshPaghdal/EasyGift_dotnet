<?php
	include 'check-seller.php';

    	$shop_id = $_SESSION['Shop_Id'];
		$limit = 7;
		$stat=3;
		$tempUrl = $url.'api/EasyGift/Shop/'.$shop_id.'/GetOrders/'.$stat.'?limit='.$limit;
		$response = fetchRequest($tempUrl);
		if($response['statusCode']==200 && count($response['result']) > 0)
		{
			$output = "
              <table id='quick-product-table' class='table table-hover dashboard-task-infos'>
               	<thead>
				<tr align = 'center'>
					<th>#</th>
					<th>Product Name</th>
					<th>Customer Name</th>
					<th>Quantity</th>
					<th>Total Price</th>
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
				$order_date = $row['OrderDate'];
				$order_date = new DateTime($order_date);
    			$order_date = $order_date->format('d-m-Y');
				$status=$row['Status'];

				$output .="<tr>
				<td>$i</td>
				<td>{$prod_name}</td>
				<td>{$cust_name}</td>
				<td>{$quantity}</td>
				<td>{$total_price}</td>";
					if ($status == 0){
						$output .= "<td>
					<center><select  class='form-control custom-select label bg-orange'
							style='border-radius: 12px 12px 12px 12px;
							height: 25px;
							width: 90px;
							font-size : 12px;
							align : center;'

							name='Order_Status_2' id = 'Order_Status_2'>";
						$output .= "<option selected>Panding </option>";
						$output .= "<option value = 'Complete/$id'>Complete</option>";
						$output .= "<option value = 'Reject/$id'>Reject</option>";
						$output .= "</select></center>
						</td>";
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

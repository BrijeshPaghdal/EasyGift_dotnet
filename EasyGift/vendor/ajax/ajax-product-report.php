<?php
	include 'check-seller.php';

    $shop_id = $_SESSION['Shop_Id'];
    $status = $_POST['status'];

    $tempUrl = $url.'api/EasyGift/Shop/'.$shop_id.'/GetProducts?Status='.$status;
    $response = fetchRequest($tempUrl);
    if($response['statusCode']==200 && count($response['result'])>0)
		{
			$output = '
              <table class="display table table-hover table-checkable order-column m-t-20 width-per-100" id="tableExport">
                <thead>
                  <tr>
                    <th>#</th>
                    <th>Product Name</th>
                    <th>Category</th>
                    <th>Price</th>
                    <th>Quantity</th>
										<th>Orders</th>
                    <th>Status</th>
                  </tr>
                </thead>';
			$i=1;
			foreach ($response['result'] as $row) {
				$prod_id = $row['Id'];
				$cate_name = $row['CategoryName'];
				$prod_name=$row['ProductName'];
				$price=$row['price'];
				$avai_qua=$row['AvailableQuantity'];
				$prod_status=$row['ProductStatus'];

        $tempUrl = $url.'api/EasyGift/Order?columns=Id&filter=ProductId=='.$prod_id.'&& Status=1';
        $res = fetchRequest($tempUrl);
        if($res['result'] != null)
        {
				  $orders =  count($res['result']);
        }
        else
        {
          $orders = 0;
        }
				$output .="
				      <tr>
                    <td>{$i}</td>
                    <td>{$prod_name}</td>
                    <td>{$cate_name}</td>
                    <td>{$price}</td>
                    <td>{$avai_qua}</td>
										<td>{$orders}</td>
                    <td>";
                    if ($prod_status == 1)
                      $output .='<span class="label bg-green shadow-style">Active</span>';
                  	else
                      $output .= "<span class='label bg-red shadow-style'>Disable</span>";

        $output .= "</td>
                </tr>";
                  $i++;

			}

				$output .= "</tbody></table></div>";
				echo $output;
		}
		else
		{
			echo '
              <table class="display table table-hover table-checkable order-column m-t-20 width-per-100" id="tableExport">
                <thead>
                  <tr>
                    <th>#</th>
                    <th>Product Name</th>
                    <th>Category</th>
                    <th>Price</th>
                    <th>Quantity</th>
										<th>Orders</th>
                    <th>Status</th>
                  </tr>
                </thead>
                <tbody>
                <tr>
                    <td colspan=10><center>No Record Available</center></td>
                </tr>
                </tbody>
                ';
								
		}
?>

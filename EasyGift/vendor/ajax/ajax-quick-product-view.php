<?php
	include 'check-seller.php';
	$shop_id = $_SESSION['Shop_Id'];
  $limit = 5;
  // $tempUrl = $url.'api/EasyGift/Product?ShopId='.$shop_id.' && ProductStatus<>2 & limit='.$limit;
  $tempUrl = $url.'api/EasyGift/Shop/'.$shop_id.'/GetProducts?limit=5';
  $response = fetchRequest($tempUrl);
    if($response['statusCode'] == 200 && count($response['result']) > 0)
    {
      $output = '<table id="quick-product-table" class="table table-hover dashboard-task-infos">
                  <thead>
                    <tr align="center">
                      <th>#</th>
                      <th>Product Name</th>
                      <th>Cost</th>
                      <th>Status</th>
                    </tr>
                  </thead><tbody>';
        foreach ($response['result'] as $row) {
        $prod_id = $row['Id'];
        $prod_name=$row['ProductName'];
        $price=$row['price'];
        $prod_status=$row['ProductStatus'];

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

        $output .= '<tr>
                      <td class="table-img center">
                          <img src="product-images/'.$image_name.'"  height=40px width=40px />
                      </td>
                      <td class="text-truncate center">'.$prod_name.'</td>
                      <td class="text-truncate center">'.$price.'</td>
                      <td class="text-truncate center">';
                      if ($prod_status == 1)
                      $output .='<span class="label bg-green shadow-style">Active</span>';
                    else
                      $output .= "<span class='label bg-red shadow-style'>Disable</span>";


                      $output .=  '</td>
                    </tr>';

      }
      $output .= "</tbody>
                </table>";
      echo $output;
    }
		else {
			$output = '<table id="quick-product-table" class="table table-hover dashboard-task-infos">
                  <thead>
                    <tr align="center">
                      <th>#</th>
                      <th>Product Name</th>
                      <th>Cost</th>
                      <th>Status</th>
                    </tr>
                  </thead><tbody>
										<tr>
											<td colspan=10>No Record Found....</td>
										</tr>';
			$output .= "</tbody>
                </table>";
      echo $output;
		}

?>

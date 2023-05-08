<?php
	include 'check-seller.php';

    	$shop_id = $_SESSION['Shop_Id'];

    $tempUrl = $url.'api/EasyGift/Shop/'.$shop_id.'/GetProducts/';
    $response = fetchRequest($tempUrl);
    if($response['statusCode']==200 && count($response['result'])>0)
    {
			$output = "<div class='table-responsive' id = 'product-list'>
              <table class='table table-hover js-basic-example contact_list js-sweetalert' >
                <thead>
                  <tr>
                    <th>#</th>
                    <th>Product Name</th>
                    <th>Category</th>
                    <th>Price</th>
                    <th>QTY</th>
                    <th>Status</th>
                    <th>Action</th>
                  </tr>
                </thead>";
			$i=1;
			foreach ($response['result'] as $row) {
        $prod_id = $row['Id'];
        $cate_name = $row['CategoryName'];
        $prod_name = $row['ProductName'];
        $price = $row['price'];
        $avai_qua = $row['AvailableQuantity'];
        $prod_status = $row['ProductStatus'];

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

				$output .="
				<tr>
                    <td class='table-img'>";

                $output .="<img src='product-images/{$image_name}' height = '40px' width= '40px' />
                    </td>
                    <input type='hidden' id='prod_id' name='prod_id' value='{$prod_id}'>
                    <td>{$prod_name}</td>";
										if($cate_name == '')
										{
											$output .='<td><span class="label bg-amber shadow-style">Select Cateogry</span></td>';
										}
										else{
											$output .="<td>{$cate_name}</td>";
										}
                  $output .= "<td>{$price}</td>
                    <td>{$avai_qua}</td>
                    <td>";
                    if ($prod_status == 1)
                      $output .='<span class="label bg-green shadow-style">Active</span>';
                  	else
                      $output .= "<span class='label bg-red shadow-style'>Disable</span>";

                 $output .= "</td>
                    <td>
                      <button class='btn tblActnBtn' onclick=\"window.location.href='./product-detail.php?prod_id=$prod_id'\">
                        <i class='material-icons'>visibility</i>
                      </button>
                      <button class='btn tblActnBtn' onclick=\"window.location.href='./edit-product.php?prod_id=$prod_id'\">
                        <i class='material-icons'>mode_edit</i>
                      </button>
                      <button class='btn tblActnBtn' data-type='confirm' id='btnProdDelete' data-id='$prod_id' >
                        <i class='material-icons'>delete</i>
                      </button>
                    </td>
                  </tr>";

			}

				$output .= "</tbody></table></div>";
				echo $output;
		}
		else
		{
			echo "<center>No Records Found.....</center>";
		}
?>
<script src="assets/js/table.min.js"></script>
<!-- Custom Js -->

<script src="assets/js/pages/tables/jquery-datatable.js"></script>
<script src="assets/js/pages/ui/dialogs.js"></script>
<!-- <script src="assets/js/pages/ui/sweetalert.min.js"></script> -->

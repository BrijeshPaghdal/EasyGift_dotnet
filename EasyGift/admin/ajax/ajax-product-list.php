<?php
require_once('./check-admin.php');
function checkShopid($input)
{
  $vale = preg_match("/^(0|[1-9][0-9]*)$/", $input);
  if (!$vale) {
    return "2";
  }
}

  if(isset($_POST['shopid']))
  {
    $shop_id = $_POST['shopid'];
  }
  else
  {
    $shop_id = 0;
  }
  $tempUrl = $url.'api/EasyGift/Shop/'.$shop_id.'/GetProducts/';
  $response = fetchRequest($tempUrl);

  $v = checkShopid($shop_id);
  if ($v != "2") {
    if($response['statusCode']==200 && count($response['result'])>0)
    {
      $output = "<div class='table-responsive' id ='product-list'>
                  <table class='table table-hover js-basic-example contact_list js-sweetalert' >
                    <thead>
                      <tr>
                        <th>Image</th>
                        <th>Product Name</th>
                        <th>Category</th>
                        <th>Price</th>
                        <th>QTY</th>
                        <th>Shop Name</th>
                        <th>Status</th>
                        <th>Action</th>
                      </tr>
                    </thead>";
      $i = 1;
      foreach ($response['result'] as $row) {
        $prod_id = $row['Id'];
        $cate_name = $row['CategoryName'];
        $prod_name = $row['ProductName'];
        $price = $row['price'];
        $avai_qua = $row['AvailableQuantity'];
        $shop_name = $row['ShopName'];
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

        $output .= "
            <tr>
                        <td class='table-img'>";

        $output .= "<img src='../vendor/product-images/{$image_name}' height = '40px' width= '40px' />
                        </td>
                        <td>{$prod_name}</td>
                        <td>{$cate_name}</td>
                        <td>{$price}</td>
                        <td>{$avai_qua}</td>
                        <td>{$shop_name}</td>
                        ";
        if ($prod_status == 1) {
          $output .= "<td>  <select  class='form-control custom-select label bg-green'
                                                                      style='border-radius: 12px 12px 12px 12px;
                                                                      height: 25px;
                                                                      width: 90px;
                                                                      font-size : 14px'
                                                                      name='prod_Status' id = 'prod_Status'>";
          $output .= "<option selected value = 'Enable/$prod_id'>Enable</option>";
          $output .= "<option value = 'Disable/$prod_id'>Disable</option>";
          $output .= "</select>
            </td>";
        } else {
          $output .= "<td>
                                              <select  class='form-control custom-select label bg-orange'
                                                          style='border-radius: 12px 12px 12px 12px;
                                                          height: 25px;
                                                          width: 90px;
                                                          font-size : 14px'
                                                          name='prod_Status' id ='prod_Status'>";
          $output .= "<option  value = 'Enable/$prod_id'>Enable</option>";
          $output .= "<option selected value = 'Disable/$prod_id'>Disable</option>";
          $output .= "</select>
                                                      </td>";
        }

        $output .= "</td>
                <td>
                <button class='btn tblActnBtn' onclick=\"window.location.href='./product-details.php?prod_id=$prod_id'\">
                  <i class='material-icons'>visibility</i>
                </button>
                </td></tr>";
      }

      $output .= "</tbody></table></div>";
      echo $output;
    } else {
      echo "<center>No Product Found.....</center>";
    }
?><script src="assets/js/table.min.js"></script>
  <!-- Custom Js -->

  <script src="assets/js/pages/tables/jquery-datatable.js"></script>
  <script src="assets/js/pages/ui/dialogs.js"></script>
<?php
} else {
  echo "2";
}
?>

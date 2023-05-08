<?php
include "check-admin.php";
$GetNewSellerUrl = $url.'api/EasyGift/Seller/GetNewSellers';
  $response = fetchRequest($GetNewSellerUrl);
  if($response['statusCode']==200)
  {
      $output = '<table class="table table-hover dashboard-task-infos">
                  <thead>
                    <tr>
                      <th>#</th>
                      <th>Name</th>
                      <th>Shop Name</th>
                      <th>Email</th>
                      <th>Status</th>
                    </tr>
                  </thead>
                  <tbody>
                  ';
      foreach ( $response['result'] as $row) {

        $seller_name = $row['sellerName'] . " ";
        $seller_name .= $row['sellerLastName'];
        $shop_name = $row['shopName'];
        $seller_image = $row['sellerImage'];
        $seller_status = $row['sellerStatus'];
        $email_id = $row['emailId'];
    
        $output .= '<tr>
              <td class="table-img" align="center">
                <img src="../vendor/seller-images/' . $seller_image . '" alt="" height="40px" width="40px" />
              </td>
              <td>' . $seller_name . '</td>
              <td>' . $shop_name . '</td>
              <td>' . $email_id . '</td>
              <td>';
        if ($seller_status == 0) {
          $output .=  '<span class="label bg-red shadow-style">Disable</span>';
        } else if ($seller_status == 1) {
          $output .=  '<span class="label bg-green shadow-style">Active</span>';
        } else if ($seller_status == 2) {
          $output .=  '<span class="label bg-orange shadow-style">Panding</span>';
        } else if ($seller_status == 3) {
          $output .=  '<span class="label bg-red shadow-style">Rejected</span>';
        }
    
        $output .=  '
              </td>
            </tr>';
    
    
        $output .= '</td></tr>';
      }
      $output .= '</tbody>
    </table>';
  echo $output;
  }
  elseif($response['statusCode']==404)
  {
      echo 'No Seller Found';
  }


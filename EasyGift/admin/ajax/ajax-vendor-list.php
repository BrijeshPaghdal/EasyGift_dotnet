<?php
require_once('./check-admin.php');
$GetAllSellerUrl = $url.'api/EasyGift/Seller/GetAllSeller';
  $response = fetchRequest($GetAllSellerUrl);
  if($response['statusCode']==200)
  {
      $cnt = count($response['result']);
      if ($cnt > 0) {
        $output = "
            <table class='table table-hover js-basic-example contact_list js-sweetalert' >
              <thead>
                <tr>
                  <th>No.</th>
                  <th>Seller Name</th>
                  <th>Phone No</th>
                  <th>Email Id</th>    
                  <th>Register Date</th>
                  <th>Seller Status</th>
                  <th>Action</th>
                </tr>
              </thead> <tbody>";
        $i = 0;
        foreach ($response['result'] as $row) {
          $seller_name = $row['SellerName'];
          $seller_id = $row['Id'];
          $phone_no = $row['SellerPhoneNo'];
          $email = $row['EmailId'];
          $seller_create_date = $row['CreatedDate'];
          $seller_status = $row['SellerStatus'];
          $seller_create_date = new DateTime($seller_create_date);
          $seller_create_date = $seller_create_date->format('d-m-Y');
          $c_d = explode(" ", $seller_create_date);
          $i = $i + 1;
      
          $output .= "<tr>
                  <td>{$i}</td>
      
                  <td>{$seller_name}</td>
                  <td>{$phone_no}</td>
                  <td>{$email}</td>
                  <td>{$c_d[0]}</td>";
          if ($seller_status == 0) {
            $output .= "<td>
                              <select  class='form-control custom-select label bg-orange' 
                                      style='border-radius: 12px 12px 12px 12px; 
                                      height: 25px; 
                                      width: 90px;
                                      font-size : 14px'
                                      name='Vendor_Status' id = 'Vendor_Status'>";
      
            $output .= "<option value = 'Active/$seller_id'>Active</option>";
            $output .= "<option selected value = 'Disable/$seller_id'>Disable</option>";
            $output .= "</select>
                                  </td>";
          } else if ($seller_status == 1) {
            $output .= "<td>
               <select  class='form-control custom-select label bg-green' 
                          style='border-radius: 12px 12px 12px 12px; 
                          height: 25px; 
                          width: 90px;
                          font-size : 14px'
                          name='Vendor_Status' id ='Vendor_Status'>";
            $output .= "<option  selected value = 'Active/$seller_id'>Active</option>";
            $output .= "<option  value = 'Disable/$seller_id'>Disable</option>";
            $output .= "</select>
                      </td>";
          } else if ($seller_status == 2) {
            $output .= "<td>
               <select  class='form-control custom-select label bg-cyan' 
                          style='border-radius: 12px 12px 12px 12px; 
                          height: 25px; 
                          width: 90px;
                          font-size : 14px'
                          name='Vendor_Status' id ='Vendor_Status'>";
            $output .= "<option selected  value = 'Confirmation/$seller_id'>Confirm</option>";
            $output .= "<option value = 'Active/$seller_id'>Active</option>";
            $output .= "<option value = 'Reject/$seller_id'>Reject</option>";
            $output .= "</select>
                      </td>";
          } else if ($seller_status == 3) {
            $output .= "<td><span class='label bg-red shadow-style'>Rejected</span></td>";
          }
      
          $output .= "</td>
                  <td>
                  <button class='btn tblActnBtn' onclick=\"window.location.href='./vendor-details.php?vendor_id=$seller_id'\">
                    <i class='material-icons'>visibility</i>
                  </button>
                  </td></tr>";
        }
      
        $output .= "</tbody></table>";
        echo $output;
      } else {
        echo "<center>No Records Found.....</center>";
      }
  }
  elseif($response['statusCode']==404)
  {
      echo 0;
  }

?>

<script src="assets/js/table.min.js"></script>
<!-- Custom Js -->

<script src="assets/js/pages/tables/jquery-datatable.js"></script>
<script src="assets/js/pages/ui/dialogs.js"></script>
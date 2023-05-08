<?php
require_once('./check-admin.php');
$GetAllCustomersUrl = $url . 'api/EasyGift/Customer/GetAllCustomers';
$response = fetchRequest($GetAllCustomersUrl);
if ($response['statusCode'] == 200) {
  if (count($response['result']) > 0) {

    $output = "
      <table class='table table-hover js-basic-example contact_list js-sweetalert' >
        <thead>
          <tr>
            <th>No.</th>
            <th>User Name</th>
            <th>Phone No</th>
            <th>Email Id</th>    
            <th>Register Date</th>
            <th>User Status</th>
          </tr>
        </thead> <tbody>";
    $i = 0;
    foreach($response['result'] as $row) {

      $cust_id = $row['Id'];
      $cust_name = $row['CustomerName'];
      $phone_no = $row['MobileNo'];
      $email = $row['EmailId'];
      $cust_create_date = $row['CreatedDate'];
      $cust_create_date = new DateTime($cust_create_date);
      $cust_create_date = $cust_create_date->format('d-m-Y');
      $cust_status = $row['CustomerStatus'];
      $c_d = explode(" ", $cust_create_date);
      $i = $i + 1;

      $output .= "<tr>
            <td>{$i}</td>   
            <td>{$cust_name}</td>
            <td>{$phone_no}</td>
            <td>{$email}</td>
            <td>{$c_d[0]}</td>";
      if ($cust_status == 1) {
        $output .= "<td>
                        <select  class='form-control custom-select label bg-green' 
                                style='border-radius: 12px 12px 12px 12px; 
                                height: 25px; 
                                width: 90px;
                                font-size : 14px'
                                name='user_status' id = 'user_status'>";
        $output .= "<option selected value = 'Enable/$cust_id'>Enable</option>";
        $output .= "<option value = 'Disable/$cust_id'>Disable</option>";
        $output .= "</select>
                            </td>";
      } else {
        $output .= "<td>
            <select  class='form-control custom-select label bg-orange' 
                    style='border-radius: 12px 12px 12px 12px; 
                    height: 25px; 
                    width: 90px;
                    font-size : 14px'
                    name='user_status' id ='user_status'>";
        $output .= "<option  value = 'Enable/$cust_id'>Enable</option>";
        $output .= "<option selected value = 'Disable/$cust_id'>Disable</option>";
        $output .= "</select>
                </td>";
      }
    }

    $output .= "</tbody></table>";
    echo $output;
  } else {
    echo "<center>No Records Found.....</center>";
  }
}
?>

<script src="assets/js/table.min.js"></script>
<!-- Custom Js -->

<script src="assets/js/pages/tables/jquery-datatable.js"></script>
<script src="assets/js/pages/ui/dialogs.js"></script>
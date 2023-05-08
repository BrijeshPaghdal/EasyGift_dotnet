<?php
require_once('./check-admin.php');
$status =  $_POST['status'];
$tempUrl = $url.'api/EasyGift/Customer/GetUserReport/'.$status;
$response = fetchRequest($tempUrl);

if ($response['statusCode']==200 && count($response['result'])>0) {
    $output = '
              <table class="display table table-hover table-checkable order-column m-t-20 width-per-100" id="tableExport">
                <thead>
                  <tr>
                  <th>No.</th>
                  <th>User Name</th>
                  <th>Phone No</th>
                  <th>Email Id</th>    
                  <th>Register Date</th>
                  <th>User Status</th>
                  </tr>
                </thead><tbody>';
    $i = 0;
    foreach ($response['result'] as $row) {
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
            $output .= "<td><span class='label bg-green shadow-style'>Active</span></td>";
        } else if ($cust_status == 0) {
            $output .= "<td><span class='label bg-red shadow-style'>Disable</span></td>";
        }
        $output .= "</tr>";
    }

    $output .= "</tbody></table></div>";
    echo $output;
} else {
    echo '
              <table class="display table table-hover table-checkable order-column m-t-20 width-per-100" id="tableExport">
                <thead>
                    <tr>
                        <th>No.</th>
                        <th>User Name</th>
                        <th>Phone No</th>
                        <th>Email Id</th>    
                        <th>Register Date</th>
                        <th>User Status</th>
                    </tr>
                </thead>
                <tbody>
                <tr>
                    <td colspan=10><center>No Record Available</center></td>
                </tr>
                </tbody>
                ';
}

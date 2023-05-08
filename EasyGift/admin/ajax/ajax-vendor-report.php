<?php
require_once('./check-admin.php');
$status = $_POST['status'];
$tempUrl = $url.'api/EasyGift/Seller/GetSellerReport/'.$status;
$response = fetchRequest($tempUrl);


if ($response['statusCode']==200 && count($response['result'])>0) {

    $output = '
              <table class="display table table-hover table-checkable order-column m-t-20 width-per-100" id="tableExport">
                <thead>
                  <tr>
                  <th>No.</th>
                  <th>Seller Name</th>
                  <th>Shop Name</th>
                  <th>Phone No</th>
                  <th>Email Id</th>    
                  <th>Register Date</th>
                  <th>Seller Status</th>
                  </tr>
                </thead><tbody>';
    $i = 0;
    foreach ($response['result'] as $row) {
        $seller_name = $row['SellerName'];
        $shop_name = $row['ShopName'];
        $phone_no = $row['SellerPhoneNo'];
        $email = $row['EmailId'];
        $seller_create_date = $row['CreatedDate'];
        $seller_create_date = new DateTime($seller_create_date);
        $seller_create_date = $seller_create_date->format('d-m-Y');
        $seller_status = $row['SellerStatus'];
        $c_d = explode(" ", $seller_create_date);
        $i = $i + 1;

        $output .= "
        <tr>
        <td>{$i}</td>
        <td>{$seller_name}</td>
        <td>{$shop_name}</td>
        <td>{$phone_no}</td>
        <td>{$email}</td>
        <td>{$c_d[0]}</td>";

        if ($seller_status == 1) {
            $output .= "<td><span class='label bg-green shadow-style'>Active</span></td>";
        } else if ($seller_status == 0) {
            $output .= "<td><span class='label bg-red shadow-style'>Disable</span></td>";
        } else if ($seller_status == 3) {
            $output .= "<td><span class='label bg-red shadow-style'>Rejected</span></td>";
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
                    <th>Seller Name</th>
                    <th>Shop Name</th>
                    <th>Phone No</th>
                    <th>Email Id</th>    
                    <th>Register Date</th>
                    <th>Seller Status</th>
                    </tr>
                </thead>
                <tbody>
                <tr>
                    <td colspan=10><center>No Record Available</center></td>
                </tr>
                </tbody>
                ';
}

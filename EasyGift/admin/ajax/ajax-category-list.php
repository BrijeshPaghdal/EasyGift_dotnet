<?php
require_once('./check-admin.php');

$GetAllCategoryUrl = $url . 'api/EasyGift/Category?filter=Id<>0';
$response = fetchRequest($GetAllCategoryUrl);
if ($response['statusCode'] == 200) {
    if (count($response['result']) > 0) {
        $output = " <table class='table table-hover js-basic-example contact_list js-sweetalert'>
        <thead>
            <tr>
                <th>Images</th>
                <th>Category Name</th>
                <th>Action</th>
            </tr>
        </thead>
        <tbody>";
        foreach ($response['result'] as $row) {
            $image_name = $row['categoryImageName'];
            $cate_name = $row['categoryName'];
            $cat_id = $row['id'];

            $output .= "<tr>
            <td class='table-img'>
                <img src='category-image/{$image_name}' alt=''  height = '40px' width= '40px'  />
            </td>
            <td>{$cate_name}</td>
            <td>
                <button class='btn tblActnBtn' data-type='confirm' id='btnCateDelete' data-id='$cat_id'>
                    <i class='material-icons'>delete</i>
                </button>
            </td>
        </tr>";
        }

        $output .= "</tbody>
        </thead>
        </table>";
        echo $output;
    } else {
        echo "<center>No Records Found.....</center>";
    }
} elseif ($response['statusCode'] == 404) {
    echo "0";
}

?>

<script src="assets/js/table.min.js"></script>

<script src="assets/js/pages/tables/jquery-datatable.js"></script>
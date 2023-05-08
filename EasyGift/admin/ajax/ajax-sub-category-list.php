<?php
    require_once('./check-admin.php');

    $tempUrl = $url.'api/EasyGift/SubCategory/GetAllSubCategory';
    $response = fetchRequest($tempUrl);
    if($response['statusCode']==200 && count($response['result'])>0)
    {
        $output = " <table class='table table-hover js-basic-example contact_list js-sweetalert'>
        <thead>
            <tr>
                <th>Images</th>
                <th>Sub Category Name</th>
                <th>Category Name</th>
                <th>Action</th>
            </tr>
        </thead>
        <tbody>
            <div class='menu'>";
        foreach ($response['result'] as $row) {
            $image_name = $row['CategoryImageName'];
            $cate_name = $row['CategoryName'];
            $sub_c_name = $row['SubCategoryName'];
            $sub_c_id  = $row['Id'];

            $output .= "<tr>
            <td class='table-img'>
                <img src='category-image/{$image_name}' alt=''  height = '40px' width= '40px'  />
            </td>
            <td>{$sub_c_name}</td>
            <td>{$cate_name}</td>
            <td>
            <button class='btn tblActnBtn' data-type='confirm' id='btnSubCateDelete' data-id='$sub_c_id'>
                <i class='material-icons'>delete</i>
            </button>
            </td>
        </tr>";
        }

        $output .= "</div></tbody>
        </thead>
        </table>";
        echo $output;
    } else {
        echo "<center>No Records Found.....</center>";
    }
?>

    <script src="assets/js/table.min.js"></script>

    <script src="assets/js/pages/tables/jquery-datatable.js"></script>

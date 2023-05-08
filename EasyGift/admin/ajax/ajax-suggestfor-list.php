<?php
require_once('./check-admin.php');
$tempUrl = $url.'api/EasyGift/Suggestion';
$response = fetchRequest($tempUrl);
if ($response['statusCode']==200 &&  count($response['result']) > 0) {
    $output = " <table class='table table-hover js-basic-example contact_list js-sweetalert'>
    <thead>
        <tr>
            <th>#</th>
            <th>Sub Category Name</th>
            <th>Gedner</th>
            <th>Minimum Age</th>
            <th>Maximum Age</th>
            <th>Action</th>
        </tr>
    </thead>
    <tbody>";
    $i = 1;
    foreach($response['result'] as $row) {
        $sugg_id = $row['id'];
        $sugg_for = $row['suggestedFor'];
        $gender = $row['gender'];
        $min_age = $row['minAge'];
        $max_age = $row['maxAge'];

        $output .= "<tr>
        <td>".$i++."</td>
        <td>{$sugg_for}</td>
        <td>{$gender}</td>
        <td>{$min_age}</td>
        <td>{$max_age}</td>
        <td>
          <button class='btn tblActnBtn' data-type='confirm' id='btnSuggDelete' data-id='$sugg_id'>
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
?>

<script src="assets/js/table.min.js"></script>

<script src="assets/js/pages/tables/jquery-datatable.js"></script>

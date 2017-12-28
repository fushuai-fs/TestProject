
var obj=[{
    "name":"酒店名称1",
    "room":[{
        "name":"物理房型名称1",
        "sellroom":[{
            "name":"房型名称1",
            "cancel":"不可取消",
        },{
            "name":"房型名称2",
            "cancel":"不可取消",
        },{
            "name":"房型名称3",
            "cancel":"不可取消",
        }]
    },{
        "name":"物理房型名称2",
        "sellroom":[{
            "name":"房型名称1",
            "cancel":"不可取消",
        },{
            "name":"房型名称2",
            "cancel":"不可取消",
        } ]
    }]
},{
    "name":"酒店名称2",
    "room":[{
        "name":"物理房型名称1",
        "sellroom":[{
            "name":"房型名称1",
            "cancel":"不可取消",
        },{
            "name":"房型名称2",
            "cancel":"不可取消",
        },{
            "name":"房型名称3",
            "cancel":"不可取消",
        }]
    },{
        "name":"物理房型名称2",
        "sellroom":[{
            "name":"房型名称1",
            "cancel":"不可取消",
        },{
            "name":"房型名称2",
            "cancel":"不可取消",
        } ]
    }]
}];

/* <tr>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
* */
var htmlstr="";
$.each(obj,function () {
    htmlstr+='<tr>';
    htmlstr+='<td>';
    htmlstr+' </tr>';
});
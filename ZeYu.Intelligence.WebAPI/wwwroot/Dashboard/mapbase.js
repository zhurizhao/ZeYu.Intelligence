var map;
var myGeo;
var labelstyle = {
    border: "1px solid #EEE",
    fontWeight: "bold",
    color: "blue",
    fontSize: "12px",
    height: "20px",
    fontFamily: "微软雅黑"
};
var label = new BMap.Label("", {
    offset: new BMap.Size(-10, 30)
});

var opts = {
    width: 500,
    // 信息窗口宽度
    //height: 80,     // 信息窗口高度
    //title: "信息窗口", // 信息窗口标题
    enableMessage: false //设置允许信息窗发送短息
};

function initMap() {
    myGeo = new BMap.Geocoder();
    map = new BMap.Map("allmap");    // 创建Map实例
    map.centerAndZoom(new BMap.Point(119.653867, 29.085649), 15);  // 初始化地图,设置中心点坐标和地图级别
    map.addControl(new BMap.MapTypeControl());   //添加地图类型控件
    map.setCurrentCity("金华");          // 设置地图显示的城市 此项是必须设置的
    map.enableScrollWheelZoom(true);     //开启鼠标滚轮缩放
}

function openInfo(content, e) {
    var p = e.target;
    var point = new BMap.Point(p.getPosition().lng, p.getPosition().lat);
    var infoWindow = new BMap.InfoWindow(content, opts); // 创建信息窗口对象 
    map.openInfoWindow(infoWindow, point); //开启信息窗口
}

function addMouseOverHandler(content, marker) {
    marker.addEventListener("mouseover", function (e) {
        openInfo(content, e)
    }
    );
}
function addMouseClickHandler(content, marker) {
    marker.addEventListener("click", function (e) {
        openInfo(content, e)
    }
    );
}

function getAddress(id, lng, lat) {
    var point = new BMap.Point(lng, lat);
    myGeo.getLocation(point, function (rs) {
        var addComp = rs.addressComponents;
        var addr = addComp.province + ", " + addComp.city + ", " + addComp.district + ", " + addComp.street + ", " + addComp.streetNumber + " 附近";
        id.innerHTML = addr;
    });
}
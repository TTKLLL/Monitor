 function MakeSameHeight(one,two){
right_height = document.getElementById(two).scrollHeight;
left_height = document.getElementById(one).scrollHeight;
right_height > left_height ? document.getElementById(one).style.height = right_height + "px" : document.getElementById(two).style.height = left_height + "px";
}
function iframeResizeHeight(frame_name,body_name,offset) {
parent.document.getElementById(frame_name).height=document.getElementById(body_name).offsetHeight+offset;
};
function Resize(){
 var frame_name="right";
 var body_name="container";
 if(parent.document.getElementById(frame_name)){
  return iframeResizeHeight(frame_name,body_name,0);
 }
}
function SetIframe()
{
  var width=(window.screen.width-170)+"px";
  $("#rigth").css({"float":"left","width":width});
}

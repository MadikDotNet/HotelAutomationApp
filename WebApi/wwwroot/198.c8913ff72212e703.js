"use strict";(self.webpackChunkhotelFront=self.webpackChunkhotelFront||[]).push([[198],{198:(_,m,u)=>{u.r(m),u.d(m,{AboutUsModule:()=>N});var r=u(6019),t=u(3668),s=u(86),p=u(1659),e=u(273),c=u(6263),g=u(9834),a=u(15),d=u(5011),v=u(6387),h=u(966);function C(n,i){1&n&&t.GkF(0)}const E=function(n,i){return{item:n,index:i}},A=function(n){return{$implicit:n}};function T(n,i){if(1&n&&(t.ynx(0),t.YNc(1,C,1,0,"ng-container",13),t.BQk()),2&n){const o=i.$implicit,l=i.index;t.oxw(3);const f=t.MAs(9);t.xp6(1),t.Q6J("ngTemplateOutlet",f)("ngTemplateOutletContext",t.VKq(5,A,t.WLB(2,E,o,l)))}}function M(n,i){1&n&&t.GkF(0)}function O(n,i){if(1&n&&(t.ynx(0),t.YNc(1,M,1,0,"ng-container",13),t.BQk()),2&n){const o=i.$implicit,l=i.index;t.oxw(3);const f=t.MAs(9);t.xp6(1),t.Q6J("ngTemplateOutlet",f)("ngTemplateOutletContext",t.VKq(5,A,t.WLB(2,E,o,l)))}}function x(n,i){if(1&n&&(t.ynx(0),t.TgZ(1,"div",11),t.YNc(2,T,2,7,"ng-container",12),t.qZA(),t.TgZ(3,"div",11),t.YNc(4,O,2,7,"ng-container",12),t.qZA(),t.BQk()),2&n){const o=t.oxw(2);t.xp6(2),t.Q6J("ngForOf",o.getFirstHalfRoomGroups()),t.xp6(2),t.Q6J("ngForOf",o.getSecondHalfRoomGroups())}}function P(n,i){if(1&n&&(t.TgZ(0,"div",9),t.YNc(1,x,5,2,"ng-container",10),t.qZA()),2&n){const o=t.oxw(),l=t.MAs(11);t.xp6(1),t.Q6J("ngIf",o.roomGroups)("ngIfElse",l)}}function B(n,i){if(1&n&&(t.TgZ(0,"div",14),t.TgZ(1,"div",15),t._UZ(2,"img",16),t.qZA(),t.TgZ(3,"div",17),t.TgZ(4,"div",18),t.TgZ(5,"p",19),t._uU(6),t.qZA(),t.TgZ(7,"div",20),t._uU(8),t.qZA(),t.qZA(),t.TgZ(9,"div",21),t.TgZ(10,"p",19),t._uU(11),t.ALo(12,"numberPipe"),t.qZA(),t.TgZ(13,"div",22),t._uU(14,"New"),t.qZA(),t.qZA(),t.qZA(),t.qZA()),2&n){const o=i.$implicit,l=t.oxw();t.xp6(2),t.s9C("src",l.imagesSrc[o.index],t.LSH),t.xp6(4),t.hij(" ",o.item.name,""),t.xp6(2),t.Oqu(o.item.description),t.xp6(3),t.hij("",t.Dn7(12,4,o.item.minPrice,"mask",1),"\u0442\u0433")}}function y(n,i){1&n&&t._UZ(0,"app-empty")}function U(n,i){1&n&&t._UZ(0,"app-loading")}let D=(()=>{class n{constructor(o,l){this.apiService=o,this.imagesService=l,this.destroy$=new e.x,this.roomGroups=[],this.loading=!0,this.imagesSrc=[]}ngOnInit(){this.apiService.getCategories(0,6).pipe((0,c.R)(this.destroy$)).subscribe(o=>{this.roomGroups=o.entities,this.roomGroups.forEach(l=>{l.fileId&&this.imagesService.getSourcePath(l.fileId).subscribe(f=>{const Z=new FileReader;Z.readAsDataURL(f),Z.onloadend=()=>{this.imagesSrc=this.imagesSrc.concat(Z.result)}})}),this.loading=!1})}getFirstHalfRoomGroups(){return this.roomGroups.slice(0,this.roomGroups.length/2)}getSecondHalfRoomGroups(){return this.roomGroups.slice(this.roomGroups.length/2,this.roomGroups.length)}ngOnDestroy(){this.destroy$.next(!0),this.destroy$.complete()}}return n.\u0275fac=function(o){return new(o||n)(t.Y36(g.G),t.Y36(a.C))},n.\u0275cmp=t.Xpm({type:n,selectors:[["app-best-rooms"]],decls:14,vars:2,consts:[[1,"container","py-5"],[1,"row","justify-content-center"],[1,"col-12"],[1,"pre-title","text-center"],[1,"main-title","text-center","mb-5"],["class","row",4,"ngIf","ngIfElse"],["roomBlock",""],["noData",""],["loadingSpinner",""],[1,"row"],[4,"ngIf","ngIfElse"],[1,"col-6"],[4,"ngFor","ngForOf"],[4,"ngTemplateOutlet","ngTemplateOutletContext"],[1,"d-flex","room-block","align-center","mb-3","cursor-pointer"],[1,"flex","me-3"],["alt","",3,"src"],[1,"flex","d-flex","flex-row","justify-content-between","w-100"],[1,"flex-3"],[1,"title"],[1,"description"],[1,"flex-1","text-end"],[1,"new-mark"]],template:function(o,l){if(1&o&&(t.TgZ(0,"div",0),t.TgZ(1,"div",1),t.TgZ(2,"div",2),t.TgZ(3,"p",3),t._uU(4,"\u041b\u044e\u043a\u0441 \u043e\u0442\u0435\u043b\u044c"),t.qZA(),t.TgZ(5,"p",4),t._uU(6,"\u041b\u0443\u0447\u0448\u0438\u0435 \u043d\u043e\u043c\u0435\u0440\u0430"),t.qZA(),t.qZA(),t.YNc(7,P,2,2,"div",5),t.qZA(),t.qZA(),t.YNc(8,B,15,8,"ng-template",null,6,t.W1O),t.YNc(10,y,1,0,"ng-template",null,7,t.W1O),t.YNc(12,U,1,0,"ng-template",null,8,t.W1O)),2&o){const f=t.MAs(13);t.xp6(7),t.Q6J("ngIf",!l.loading)("ngIfElse",f)}},directives:[r.O5,r.sg,r.tP,d.T,v.N],pipes:[h.$],styles:['.room-block[_ngcontent-%COMP%]   img[_ngcontent-%COMP%]{width:5vw;height:70px}.room-block[_ngcontent-%COMP%]   .title[_ngcontent-%COMP%]{font-family:"El Messiri",sans-serif;font-size:1.3vw}.room-block[_ngcontent-%COMP%]   .description[_ngcontent-%COMP%]{color:#878787;font-weight:300}.room-block[_ngcontent-%COMP%]   .new-mark[_ngcontent-%COMP%]{border:solid 1px rgba(135,135,135,.3);text-transform:uppercase;color:#87878799;padding:0 .5vw;text-align:center;font-size:.78vw}']}),n})();var b=u(4488);let F=(()=>{class n{constructor(){}ngOnInit(){}}return n.\u0275fac=function(o){return new(o||n)},n.\u0275cmp=t.Xpm({type:n,selectors:[["app-contact-us"]],decls:29,vars:0,consts:[[1,"bg","py-5"],[1,"container","white-bg","p-5"],[1,"row"],[1,"col-5"],["mat-flat-button","",1,"secondary-button","mb-3"],[1,"title"],[1,"description"],[1,"d-flex","justify-content-between","mt-5"],[1,"flex","label"],[1,"flex","text-end","text"],[1,"d-flex","justify-content-between"],[1,"col-7"]],template:function(o,l){1&o&&(t.TgZ(0,"div",0),t.TgZ(1,"div",1),t.TgZ(2,"div",2),t.TgZ(3,"div",3),t.TgZ(4,"button",4),t._uU(5,"\u0421\u0432\u044f\u0437\u0430\u0442\u044c\u0441\u044f \u0441 \u043d\u0430\u043c\u0438"),t.qZA(),t.TgZ(6,"p",5),t._uU(7,"\u041e\u0441\u0442\u0430\u0432\u044c\u0442\u0435 \u0441\u0432\u043e\u0439 \u043e\u0442\u0437\u044b\u0432"),t.qZA(),t.TgZ(8,"p",6),t._uU(9,"\u0414\u043b\u044f \u043f\u043e\u043b\u0443\u0447\u0435\u043d\u0438\u044f \u0434\u043e\u043f\u043e\u043b\u043d\u0438\u0442\u0435\u043b\u044c\u043d\u043e\u0439 \u0438\u043d\u0444\u043e\u0440\u043c\u0430\u0446\u0438\u0438 \u0438\u043b\u0438 \u0431\u0440\u043e\u043d\u0438\u0440\u043e\u0432\u0430\u043d\u0438\u044f, \u043f\u043e\u0436\u0430\u043b\u0443\u0439\u0441\u0442\u0430, \u0437\u0430\u043f\u043e\u043b\u043d\u0438\u0442\u0435 \u0444\u043e\u0440\u043c\u0443 \u0438\u043b\u0438 \u0441\u0432\u044f\u0436\u0438\u0442\u0435\u0441\u044c \u0441 \u043d\u0430\u043c\u0438"),t.qZA(),t.TgZ(10,"div",7),t.TgZ(11,"div",8),t._uU(12,"\u0410\u0434\u0440\u0435\u0441:"),t.qZA(),t.TgZ(13,"div",9),t._uU(14,"\u0443\u043b.\u041a\u043e\u0448\u043a\u0430\u0440\u0431\u0430\u0435\u0432\u0430 7"),t.qZA(),t.qZA(),t._UZ(15,"hr"),t.TgZ(16,"div",10),t.TgZ(17,"div",8),t._uU(18,"\u0413\u043e\u0440\u043e\u0434:"),t.qZA(),t.TgZ(19,"div",9),t._uU(20,"\u041d\u0443\u0440-\u0421\u0443\u043b\u0442\u0430\u043d"),t.qZA(),t.qZA(),t._UZ(21,"hr"),t.TgZ(22,"div",10),t.TgZ(23,"div",8),t._uU(24,"E-mail:"),t.qZA(),t.TgZ(25,"div",9),t._uU(26,"hotelbooking@gmail.com"),t.qZA(),t.qZA(),t.qZA(),t.TgZ(27,"div",11),t._UZ(28,"app-appeal-form"),t.qZA(),t.qZA(),t.qZA(),t.qZA())},directives:[s.lW,b.n],styles:['[_nghost-%COMP%]     .mat-form-field-appearance-fill .mat-form-field-flex{padding-top:0;border:solid .1vw #e5e5e5;border-radius:.1vw}[_nghost-%COMP%]     .mat-form-field-underline{display:none}.bg[_ngcontent-%COMP%]{background-image:url(feedback-bg.4cd954a2aece2d45.jpg);background-repeat:no-repeat;background-position:center;background-size:cover;height:80vh}.white-bg[_ngcontent-%COMP%]{background:#f9f9f9;height:100%;width:70vw}.white-bg[_ngcontent-%COMP%]   .secondary-button[_ngcontent-%COMP%]{padding:0 .5vw!important;font-size:.58vw!important;line-height:3vh}.white-bg[_ngcontent-%COMP%]   .title[_ngcontent-%COMP%]{font-size:2vw;font-family:"El Messiri",sans-serif}.white-bg[_ngcontent-%COMP%]   .description[_ngcontent-%COMP%]{color:#878787;line-height:3vh;letter-spacing:.1vw;font-weight:300}.white-bg[_ngcontent-%COMP%]   .label[_ngcontent-%COMP%]{font-weight:300}.white-bg[_ngcontent-%COMP%]   .text[_ngcontent-%COMP%]{font-weight:300;color:#878787}.white-bg[_ngcontent-%COMP%]   hr[_ngcontent-%COMP%]{opacity:.1}']}),n})(),w=(()=>{class n{constructor(){}ngOnInit(){}}return n.\u0275fac=function(o){return new(o||n)},n.\u0275cmp=t.Xpm({type:n,selectors:[["app-about-us"]],decls:11,vars:1,consts:[[1,"header-banner"],[1,"background"],[1,"title"],[1,"text","mb-5"],[1,"description"],["mat-flat-button","",1,"secondary-button",3,"routerLink"]],template:function(o,l){1&o&&(t.TgZ(0,"div",0),t._UZ(1,"div",1),t.TgZ(2,"div",2),t.TgZ(3,"div",3),t._uU(4," \u041e \u043d\u0430\u0441"),t.qZA(),t.TgZ(5,"p",4),t._uU(6,"\u041c\u044b \u043f\u0440\u043e\u044f\u0432\u043b\u044f\u0435\u043c \u0437\u0430\u0431\u043e\u0442\u0443 \u043e \u043a\u043b\u0438\u0435\u043d\u0442\u0430\u0445, \u043f\u0440\u0435\u0434\u043b\u0430\u0433\u0430\u044f \u0438\u043c \u0432\u044b\u0441\u043e\u043a\u043e\u043a\u043b\u0430\u0441\u0441\u043d\u043e\u0435 \u043e\u0431\u0441\u043b\u0443\u0436\u0438\u0432\u0430\u043d\u0438\u0435. \u041d\u043e\u043c\u0435\u0440\u043d\u043e\u0439 \u0444\u043e\u043d\u0434 \u043e\u0442\u0435\u043b\u044f \u043f\u043e\u0437\u0432\u043e\u043b\u0438\u0442 \u043d\u0430\u043c \u043e\u0434\u043e\u0431\u0440\u0438\u0442\u044c \u0434\u043b\u044f \u0412\u0430\u0441 \u043d\u043e\u043c\u0435\u0440, \u043a\u043e\u0442\u043e\u0440\u044b\u0439 \u0443\u0434\u043e\u0432\u043b\u0435\u0442\u0432\u043e\u0440\u0438\u0442 \u0432\u0441\u0435 \u0412\u0430\u0448\u0438 \u043f\u043e\u0436\u0435\u043b\u0430\u043d\u0438\u044f \u043a \u043a\u043e\u043c\u0444\u043e\u0440\u0442\u0443 \u0438 \u0443\u0440\u043e\u0432\u043d\u044e \u0441\u0435\u0440\u0432\u0438\u0441\u0430. "),t.qZA(),t.TgZ(7,"button",5),t._uU(8,"\u0421\u0432\u044f\u0437\u0430\u0442\u044c\u0441\u044f \u0441 \u043d\u0430\u043c\u0438"),t.qZA(),t.qZA(),t.qZA(),t._UZ(9,"app-best-rooms"),t._UZ(10,"app-contact-us")),2&o&&(t.xp6(7),t.Q6J("routerLink","/portal/contacts"))},directives:[s.lW,p.rH,D,F],styles:[".title[_ngcontent-%COMP%]{font-size:2vh}"]}),n})();var I=u(8167),R=u(138),S=u(4356),L=u(1382);const $=[{path:"",component:w,pathMatch:"prefix"}];let N=(()=>{class n{}return n.\u0275fac=function(o){return new(o||n)},n.\u0275mod=t.oAB({type:n}),n.\u0275inj=t.cJS({imports:[[r.ez,p.Bz.forChild($),s.ot,I.lN,R.c,S.D,L.m]]}),n})()},5011:(_,m,u)=>{u.d(m,{T:()=>t});var r=u(3668);let t=(()=>{class s{constructor(){}ngOnInit(){}}return s.\u0275fac=function(e){return new(e||s)},s.\u0275cmp=r.Xpm({type:s,selectors:[["app-empty"]],decls:2,vars:0,consts:[[1,"d-flex","h-100","align-center","justify-center","flex-1"]],template:function(e,c){1&e&&(r.TgZ(0,"h3",0),r._uU(1,"\u041d\u0435\u0442 \u0434\u0430\u043d\u043d\u044b\u0445"),r.qZA())},styles:["[_nghost-%COMP%] {display:contents}[_nghost-%COMP%]  h3{font-size:.7vw;font-weight:400;opacity:.75}"]}),s})()},6387:(_,m,u)=>{u.d(m,{N:()=>p});var r=u(3668),t=u(6019),s=u(7964);let p=(()=>{class e{constructor(){}ngOnInit(){}}return e.\u0275fac=function(g){return new(g||e)},e.\u0275cmp=r.Xpm({type:e,selectors:[["app-loading"]],inputs:{class:"class"},decls:2,vars:1,consts:[[1,"d-flex","flex-1","align-center","justify-center","py-20",3,"ngClass"]],template:function(g,a){1&g&&(r.TgZ(0,"div",0),r._UZ(1,"mat-spinner"),r.qZA()),2&g&&r.Q6J("ngClass",a.class)},directives:[t.mk,s.$g],styles:["[_nghost-%COMP%] {display:contents}[_nghost-%COMP%]  .mat-spinner, [_nghost-%COMP%]  svg{width:50px!important;height:50px!important}.full[_ngcontent-%COMP%]{width:100%;height:100%}"]}),e})()},4356:(_,m,u)=>{u.d(m,{D:()=>s});var r=u(6019),t=u(3668);let s=(()=>{class p{}return p.\u0275fac=function(c){return new(c||p)},p.\u0275mod=t.oAB({type:p}),p.\u0275inj=t.cJS({imports:[[r.ez]]}),p})()},966:(_,m,u)=>{u.d(m,{$:()=>t});var r=u(3668);let t=(()=>{class s{constructor(){}transform(e,c,g,a){if(isNaN(e))return e;if("number"==typeof e){if("round"===c&&!this.isInteger(e))return e.toFixed(1);if("space"===c)return e.toString().replace(/\B(?=(\d{3})+(?!\d))/g," ");if(c.includes("round-")){const d=parseInt(c.split("-")[1]);if(-1!==e.toString().indexOf(".")){const h=e.toString().split("."),C=h[1].substring(0,d);return h[0]+"."+C}return e}if("mask"===c){const d=g||0;return this.isInteger(e)||(e=g>1?Math.round(e*Math.pow(10,d))/Math.pow(10,d):e.toFixed(d)),e.toString().replace(/\B(?=(\d{3})+(?!\d))/g," ")}}else if("string"==typeof e&&(e=Number(e),"mask"===c)){const d=g||0;return this.isInteger(e)||(e=Math.round(e*Math.pow(10,d))/Math.pow(10,d)),e}return e}isInteger(e){return e%1==0}}return s.\u0275fac=function(e){return new(e||s)},s.\u0275pipe=r.Yjl({name:"numberPipe",type:s,pure:!0}),s})()},15:(_,m,u)=>{u.d(m,{C:()=>e});var r=u(4522),t=u(8260),s=u(3668),p=u(7869);let e=(()=>{class c{constructor(a,d){this.http=a,this.httpClient=d,this.rootApi="File/",this.apiUrl=t.N.apiUrl,this.headers=(new r.WM).set("Authorization",`Bearer ${localStorage.getItem("token")}`)}getSourcePath(a){return this.httpClient.get(`${this.apiUrl}${this.rootApi}Download?MediaId=${a}`,{headers:{Authorization:`Bearer ${localStorage.getItem("token")}`,"Content-Type":"application/octet-stream"},responseType:"blob"})}uploadImage(a){return this.httpClient.post(`${this.apiUrl}${this.rootApi}Upload`,a,{headers:this.headers,responseType:"text"})}deleteFile(a){return this.http.delete(`${this.rootApi}Delete/${a}`)}imagePath(a){return`data:${null==a?void 0:a.fileType};base64,${null==a?void 0:a.data}`}}return c.\u0275fac=function(a){return new(a||c)(s.LFG(p.O),s.LFG(r.eN))},c.\u0275prov=s.Yz7({token:c,factory:c.\u0275fac,providedIn:"root"}),c})()}}]);
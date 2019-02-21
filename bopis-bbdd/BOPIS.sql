CREATE TABLE "BOPIS"."Configuration" (
  "id" NUMBER(10) NOT NULL ,
  "description" VARCHAR2(50) NOT NULL ,
  "value" VARCHAR2(500) NOT NULL ,
  "key" VARCHAR2(500) NOT NULL ,
  "dataType" VARCHAR2(50) NOT NULL ,
  "readOnly" NUMBER(1) NOT NULL ,
  "status" NUMBER(1) NOT NULL ,
  PRIMARY KEY("id")
);

CREATE TABLE "BOPIS"."Cylinder" (
  "id" NUMBER(10) NOT NULL ,
  "name" VARCHAR2(100) NOT NULL ,
  "kg" NUMBER(2) NOT NULL ,
  "image" VARCHAR2(50) NOT NULL ,
  "status" NUMBER(1) NOT NULL ,
  PRIMARY KEY("id")
);

CREATE TABLE "BOPIS"."Local" (
  "id" NUMBER(10) NOT NULL ,
  "name" VARCHAR2(100) NOT NULL ,
  "latitude" FLOAT(126) NOT NULL ,
  "length" FLOAT(126) NOT NULL ,
  "radio" FLOAT(126) NOT NULL ,
  "open" NUMBER(1) NOT NULL ,
  "status" NUMBER(1) NOT NULL ,
  PRIMARY KEY("id")
);

CREATE TABLE "BOPIS"."CylinderByLocal" (
  "id" NUMBER(10) NOT NULL ,
  "localId" NUMBER(10) NOT NULL ,
  "cylinderId" NUMBER(10) NOT NULL ,
  "zonePrice" NUMBER(10) NOT NULL ,
  "discount" NUMBER(5) NOT NULL ,
  "finalPrice" NUMBER(10) NOT NULL ,
  "status" NUMBER(1) NOT NULL ,
  PRIMARY KEY("id") ,
  FOREIGN KEY("localId") REFERENCES "BOPIS"."Local"("id") ,
  FOREIGN KEY("cylinderId") REFERENCES "BOPIS"."Cylinder"("id")
);

CREATE TABLE "BOPIS"."TypeLog" (
  "id" NUMBER(10) NOT NULL ,
  "description" VARCHAR2(50) NOT NULL ,
  "status" NUMBER(1) NOT NULL ,
  PRIMARY KEY("id")
);

CREATE TABLE "BOPIS"."Role" (
  "id" NUMBER(10) NOT NULL ,
  "description" VARCHAR2(100) NOT NULL ,
  "status" NUMBER(1) NOT NULL ,
  PRIMARY KEY("id")
);

CREATE TABLE "BOPIS"."Profile" (
  "id" NUMBER(10) NOT NULL ,
  "description" VARCHAR2(50) NOT NULL ,
  "status" NUMBER(1) NOT NULL ,
  PRIMARY KEY("id")
);

CREATE TABLE "BOPIS"."User" (
  "id" NUMBER(10) NOT NULL ,
  "profileId" NUMBER(10) NOT NULL ,
  "localId" NUMBER(10) ,
  "fullName" VARCHAR2(500) NOT NULL ,
  "run" VARCHAR2(30) NOT NULL ,
  "email" VARCHAR2(50) NOT NULL ,
  "password" VARCHAR2(500) NOT NULL ,
  "status" NUMBER(1) NOT NULL ,
  PRIMARY KEY("id") ,
  FOREIGN KEY("profileId") REFERENCES "BOPIS"."Profile"("id") ,
  FOREIGN KEY("localId") REFERENCES "BOPIS"."Local"("id")
);

CREATE TABLE "BOPIS"."Log" (
  "id" NUMBER(10) NOT NULL ,
  "typeLogId" NUMBER(10) NOT NULL ,
  "userId" NUMBER(10) ,
  "controller" VARCHAR2(100) NOT NULL ,
  "method" VARCHAR2(100) NOT NULL ,
  "description" VARCHAR2(500) NOT NULL ,
  "date" DATE NOT NULL ,
  PRIMARY KEY("id") ,
  FOREIGN KEY("typeLogId") REFERENCES "BOPIS"."TypeLog"("id") ,
  FOREIGN KEY("userId") REFERENCES "BOPIS"."User"("id")
);

CREATE TABLE "BOPIS"."ProfileRole" (
  "id" NUMBER(10) NOT NULL ,
  "profileId" NUMBER(10) NOT NULL ,
  "roleId" NUMBER(10) NOT NULL ,
  PRIMARY KEY("id") ,
  FOREIGN KEY("profileId") REFERENCES "BOPIS"."Profile"("id") ,
  FOREIGN KEY("roleId") REFERENCES "BOPIS"."Role"("id")
);

CREATE TABLE "BOPIS"."OrderStatus" (
  "id" NUMBER(10) NOT NULL ,
  "description" VARCHAR2(50) NOT NULL ,
  "status" NUMBER(1) NOT NULL ,
  PRIMARY KEY("id")
);

CREATE TABLE "BOPIS"."Week" (
  "id" NUMBER(10) NOT NULL ,
  "dayOfWeek" VARCHAR2(20) NOT NULL ,
  "status" NUMBER(1) NOT NULL ,
  PRIMARY KEY("id")
);

CREATE TABLE "BOPIS"."Stock" (
  "id" NUMBER(10) NOT NULL ,
  "cylinderByLocalId" NUMBER(10) NOT NULL ,
  "quantity" NUMBER(10) NOT NULL ,
  "status" NUMBER(1) NOT NULL ,
  PRIMARY KEY("id") ,
  FOREIGN KEY("cylinderByLocalId") REFERENCES "BOPIS"."CylinderByLocal"("id")
);

CREATE TABLE "BOPIS"."ScheduleOfAttention" (
  "id" NUMBER(10) NOT NULL ,
  "localId" NUMBER(10) NOT NULL ,
  "weekId" NUMBER(10) NOT NULL ,
  "start" VARCHAR2(5) NOT NULL ,
  "finish" VARCHAR2(5) NOT NULL ,
  "status" NUMBER(1) ,
  PRIMARY KEY("id") ,
  FOREIGN KEY("localId") REFERENCES "BOPIS"."Local"("id") ,
  FOREIGN KEY("weekId") REFERENCES "BOPIS"."Week"("id")
);

CREATE TABLE "BOPIS"."Order" (
  "id" NUMBER(10) NOT NULL ,
  "orderStatusId" NUMBER(10) NOT NULL ,
  "cylinderByLocalId" NUMBER(10) NOT NULL ,
  "userId" NUMBER(10) NOT NULL ,
  "clientRun" VARCHAR2(30) NOT NULL ,
  "clientAddress" VARCHAR2(500) NOT NULL ,
  "clientFullName" VARCHAR2(500) NOT NULL ,
  "quantity" NUMBER(2) NOT NULL ,
  "scheduleOfRetirement" VARCHAR2(20) ,
  "dateOfDelivery" DATE ,
  "dateOfRequest" DATE NOT NULL ,
  PRIMARY KEY("id") ,
  FOREIGN KEY("orderStatusId") REFERENCES "BOPIS"."OrderStatus"("id") ,
  FOREIGN KEY("cylinderByLocalId") REFERENCES "BOPIS"."CylinderByLocal"("id") ,
  FOREIGN KEY("userId") REFERENCES "BOPIS"."User"("id")
);



INSERT INTO "BOPIS"."Configuration" VALUES ('1', 'Token Expiration By Minutes', '1440', 'TOKEN', 'NUMERIC', '0', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('2', 'Token Key', 'UFJPWUVDVE8gQ09OVFJPTExFUiBQQVJBIEFSQ0FESVMgREUgT1BFTlMgU09MVVRJT05TIA==', 'TOKEN', 'VARCHAR', '0', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('3', 'Email From', 'portafolio.diego.paredes@gmail.com', 'EMAIL', 'VARCHAR', '0', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('4', 'Email Password', 'Catolica10.', 'EMAIL', 'VARCHAR', '0', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('5', 'Email Name', 'Diego Felipe Paredes Villalobos', 'EMAIL', 'VARCHAR', '0', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('6', 'Email Port', '587', 'EMAIL', 'NUMERIC', '0', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('7', 'Email Smtp', 'smtp.gmail.com', 'EMAIL', 'VARCHAR', '0', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('8', 'Email Template Recovery Password', 'C:\inetpub\wwwroot\bopis-api\Template\template-recovery-password.html', 'EMAIL', 'VARCHAR', '0', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('9', 'Cancellation Of Orders By Hours', '24', 'SYSTEM', 'NUMERIC', '0', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('10', 'Random Password Length', '10', 'SYSTEM', 'NUMERIC', '0', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('11', 'Random Password Characters', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789', 'SYSTEM', 'VARCHAR', '0', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('12', 'Number Of Records By Data', '10', 'SYSTEM', 'NUMERIC', '0', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('13', 'Id Available For The User Table', '2', 'BD', 'NUMERIC', '1', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('14', 'Id Available For The Log Table', '1', 'BD', 'NUMERIC', '1', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('15', 'Id Available For The Order Table', '1', 'BD', 'NUMERIC', '1', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('16', 'Id Available For The SheduleOfAttention Table', '1', 'BD', 'NUMERIC', '1', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('17', 'Id Available For The Local Table', '1', 'BD', 'NUMERIC', '1', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('18', 'Id Available For The CylinderByLocal Table', '1', 'BD', 'NUMERIC', '1', '1');
INSERT INTO "BOPIS"."Configuration" VALUES ('19', 'Id Available For The Stock Table', '1', 'BD', 'NUMERIC', '1', '1');

INSERT INTO "BOPIS"."Cylinder" VALUES ('1', 'GAS 11KG PLUS', '11', 'assets/img/11plus.jpg', '1');
INSERT INTO "BOPIS"."Cylinder" VALUES ('2', 'GAS 05KG', '5', 'assets/img/5.jpg', '1');
INSERT INTO "BOPIS"."Cylinder" VALUES ('3', 'GAS 11KG', '11', 'assets/img/11.jpg', '1');
INSERT INTO "BOPIS"."Cylinder" VALUES ('4', 'GAS 15KG', '15', 'assets/img/15.jpg', '1');
INSERT INTO "BOPIS"."Cylinder" VALUES ('5', 'GAS 45KG', '45', 'assets/img/45.jpg', '1');

INSERT INTO "BOPIS"."TypeLog" VALUES ('1', 'Events', '1');
INSERT INTO "BOPIS"."TypeLog" VALUES ('2', 'Exceptions', '1');

INSERT INTO "BOPIS"."Role" VALUES ('1', 'Inicio', '1');
INSERT INTO "BOPIS"."Role" VALUES ('2', 'Buscador', '1');
INSERT INTO "BOPIS"."Role" VALUES ('3', 'Opciones', '1');
INSERT INTO "BOPIS"."Role" VALUES ('4', 'Precios', '1');
INSERT INTO "BOPIS"."Role" VALUES ('5', 'Locales', '1');
INSERT INTO "BOPIS"."Role" VALUES ('6', 'Usuarios', '1');
INSERT INTO "BOPIS"."Role" VALUES ('7', 'Reportes', '1');
INSERT INTO "BOPIS"."Role" VALUES ('8', 'Configuraciones', '1');

INSERT INTO "BOPIS"."Profile" VALUES ('1', 'Administrador', '1');
INSERT INTO "BOPIS"."Profile" VALUES ('2', 'Usuario', '1');
INSERT INTO "BOPIS"."Profile" VALUES ('3', 'Super Administrador', '1');

INSERT INTO "BOPIS"."User" VALUES ('1', '3', NULL, 'Diego Felipe Paredes Villalobos', '19.558.913-5', 'diego.paredes@opens.cl', '$2a$10$4w9ItOnJECYdCSuA81FqlOcS55I8wDAE8UaQySTgDlif7zJ42unQy', '1');

INSERT INTO "BOPIS"."ProfileRole" VALUES ('1', '1', '1');
INSERT INTO "BOPIS"."ProfileRole" VALUES ('2', '1', '5');
INSERT INTO "BOPIS"."ProfileRole" VALUES ('3', '1', '6');
INSERT INTO "BOPIS"."ProfileRole" VALUES ('4', '1', '7');
INSERT INTO "BOPIS"."ProfileRole" VALUES ('5', '2', '1');
INSERT INTO "BOPIS"."ProfileRole" VALUES ('6', '2', '2');
INSERT INTO "BOPIS"."ProfileRole" VALUES ('7', '2', '3');
INSERT INTO "BOPIS"."ProfileRole" VALUES ('8', '2', '4');
INSERT INTO "BOPIS"."ProfileRole" VALUES ('9', '3', '1');
INSERT INTO "BOPIS"."ProfileRole" VALUES ('10', '3', '2');
INSERT INTO "BOPIS"."ProfileRole" VALUES ('11', '3', '3');
INSERT INTO "BOPIS"."ProfileRole" VALUES ('12', '3', '4');
INSERT INTO "BOPIS"."ProfileRole" VALUES ('13', '3', '5');
INSERT INTO "BOPIS"."ProfileRole" VALUES ('14', '3', '6');
INSERT INTO "BOPIS"."ProfileRole" VALUES ('15', '3', '7');
INSERT INTO "BOPIS"."ProfileRole" VALUES ('16', '3', '8');

INSERT INTO "BOPIS"."OrderStatus" VALUES ('1', 'Entregados', '1');
INSERT INTO "BOPIS"."OrderStatus" VALUES ('2', 'Pendientes', '1');
INSERT INTO "BOPIS"."OrderStatus" VALUES ('3', 'Anulados', '1');

INSERT INTO "BOPIS"."Week" VALUES ('1', 'Lunes', '1');
INSERT INTO "BOPIS"."Week" VALUES ('2', 'Martes', '1');
INSERT INTO "BOPIS"."Week" VALUES ('3', 'Miércoles', '1');
INSERT INTO "BOPIS"."Week" VALUES ('4', 'Jueves', '1');
INSERT INTO "BOPIS"."Week" VALUES ('5', 'Viernes', '1');
INSERT INTO "BOPIS"."Week" VALUES ('6', 'Sábado', '1');
INSERT INTO "BOPIS"."Week" VALUES ('7', 'Domingo', '1');
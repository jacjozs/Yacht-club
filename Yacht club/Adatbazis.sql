SET FOREIGN_KEY_CHECKS=0;
  
CREATE TABLE zip_codes (
  zip_code int (4) NOT NULL,
  city varchar(30),
  PRIMARY KEY (zip_code))
  ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE yacht_club_tags (
  tag_id int(6) NOT NULL,
  name varchar(55) NOT NULL,
  image int (2),
  yacht_ids int(255) DEFAULT NULL,
  zip_code int(4) NOT NULL,
  home_adress varchar(50) NOT NULL,
  birthday datetime NOT NULL,
  devices int(255) DEFAULT NULL,
  PRIMARY KEY (tag_id), 
  FOREIGN KEY (yacht_ids) REFERENCES yachts (yacht_id) ON DELETE CASCADE ON UPDATE CASCADE, FOREIGN KEY (zip_code) REFERENCES zip_codes (zip_code)
  ) ENGINE=InnoDB DEFAULT CHARSET=utf8;

  CREATE TABLE yachts (
  yacht_id int(4) NOT NULL,
  name varchar(30) NOT NULL,
  image int(2) DEFAULT NULL,
  ower int(6) NOT NULL,
  now_ower int (6) NOT NULL,
  seats int (4),
  busy int(1) DEFAULT '0',
  PRIMARY KEY (yacht_id),
  FOREIGN KEY (ower) REFERENCES yacht_club_tags (tag_id) on update cascade on delete cascade,
  FOREIGN KEY (now_ower) REFERENCES yacht_club_tags (tag_id) on update cascade on delete cascade
  ) ENGINE=InnoDB DEFAULT CHARSET=utf8;

  CREATE TABLE loan_yacht (
  loan_id int (8) NOT NULL,
  who int (6) NOT NULL,
  for_who int(6) NOT NULL,
  from_time datetime NOT NULL,
  finish_time datetime NOT NULL, 
  yacht_id int (4) NOT NULL,
  PRIMARY KEY (loan_id),
  FOREIGN KEY (who) REFERENCES yacht_club_tags (tag_id) ON DELETE CASCADE ON UPDATE CASCADE, FOREIGN KEY (for_who) REFERENCES yacht_club_tags(tag_id) ON DELETE CASCADE ON UPDATE CASCADE, 
  FOREIGN KEY (yacht_id) REFERENCES yachts(yacht_id) ON DELETE CASCADE ON UPDATE CASCADE 
  ) ENGINE=InnoDB DEFAULT CHARSET=utf8;

  CREATE TABLE travels (
  yacht_id int (4) NOT NULL,
  yacht_ower int (6) NOT NULL,
  where_old_time datetime NOT NULL,
  where_is varchar(50) NOT NULL,
  where_old varchar(50) NOT NULL,
  PRIMARY KEY (yacht_id, yacht_ower, where_old_time),
  FOREIGN KEY(yacht_ower) REFERENCES yacht_club_tags (tag_id) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY(yacht_id) REFERENCES yachts (yacht_id) ON DELETE CASCADE ON UPDATE CASCADE 
  ) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE transport_device (
  device_id int (4) NOT NULL,
  ower int (6) NOT NULL,
  now_ower int (6) NOT NULL,
  size int(4) NOT NULL,
  busy int (1) DEFAULT '0',
  PRIMARY KEY (device_id),
  FOREIGN KEY (ower) REFERENCES yacht_club_tags (tag_id) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (now_ower) REFERENCES yacht_club_tags (tag_id) ON DELETE CASCADE ON UPDATE CASCADE
  ) ENGINE=InnoDB DEFAULT CHARSET=utf8;

  CREATE TABLE loan_device (
  loan_id int (8) NOT NULL,
  who int (6) NOT NULL,
  for_who int(6) NOT NULL,
  from_time datetime NOT NULL,
  finish_time datetime NOT NULL, 
  device_id int (4) NOT NULL,
  PRIMARY KEY (loan_id),
  FOREIGN KEY (who) REFERENCES yacht_club_tags (tag_id) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (for_who) REFERENCES yacht_club_tags (tag_id) ON DELETE CASCADE ON UPDATE CASCADE, 
  FOREIGN KEY (device_id) REFERENCES transport_device (device_id) ON DELETE CASCADE ON UPDATE CASCADE
  ) ENGINE=InnoDB DEFAULT CHARSET=utf8;

  CREATE TABLE yacht_login (
  login_id int(10) NOT NULL,
  club_tag_id int (6) NOT NULL,
  password varchar(65) NOT NULL,
  name varchar(25) NOT NULL,
  access_level int(1) NOT NULL,
  last_login datetime NOT NULL,
  PRIMARY KEY (login_id),
  FOREIGN KEY (club_tag_id) REFERENCES yacht_club_tags (tag_id) ON DELETE CASCADE ON UPDATE CASCADE
  ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
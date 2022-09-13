CREATE TABLE [IF NOT EXISTS] SampleTable (
  `id` INT UNSIGNED AUTO_INCREMENT,
  `title` VARCHAR(100) NOT NULL,
  `author` VARCHAR(40) NOT NULL,
  `createDate` DATE,
  PRIMARY KEY ( `id` )
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

comment on column SampleTable.title is '標題';
comment on column SampleTable.author is '作者';
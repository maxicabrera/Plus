'use strict';

module.exports = function(grunt) {

  require('load-grunt-tasks')(grunt);


  grunt.initConfig({
    sass: {
      dist: {
        options: {
          style: 'expanded', // expanded or nested or compact or compressed
          loadPath: './',
          compass: true,
          quiet: false,
          lineComments: false
        },
        files: {
          './app.css': './main.scss'
        }
      }
    },
    watch: {


      sass: {
        files: './*.scss',
        tasks: ['sass']
      }
    }
  });


  grunt.registerTask('default', ['watch']);

};

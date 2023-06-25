'use strict';

const gulp = require('gulp'),
    cssnano = require('cssnano'),
    autoprefixer = require('autoprefixer'),
    terser = require('gulp-terser'),
    inject = require('gulp-inject-string'),
    sourcemaps = require('gulp-sourcemaps'),
    sass = require('gulp-dart-sass'),
    postcss = require('gulp-postcss'),
    rimraf = require('rimraf'),
    rename = require('gulp-rename'),
    concat = require('gulp-concat'),
    merge = require('merge-stream'),
    bundleconfig = require('./bundleconfig.json');

sass.compiler = require('sass');

const paths = {
    scss: './wwwroot/scss',
    js: './wwwroot/js',
    dist: './wwwroot/dist'
};

const now = Date();

const getBundles = (regexPattern) => {
    return bundleconfig.filter(bundle => {
        return regexPattern.test(bundle.outputFileName);
    });
};

gulp.task('bundle-libs:js', async () => {
    merge(getBundles(/\.js$/).map(bundle => {
        return gulp.src(bundle.inputFiles, {base: '.'})
            .pipe(concat(bundle.outputFileName))
            .pipe(gulp.dest(paths.dist))
            .pipe(terser())
            .pipe(rename({ extname: '.min.js' }))
            .pipe(gulp.dest(paths.dist));
    }));
});

gulp.task('bundle-libs:css', async () => {
    merge(getBundles(/\.css$/).map(bundle => {
        return gulp.src(bundle.inputFiles, {base: '.'})
            .pipe(concat(bundle.outputFileName))
            .pipe(postcss([cssnano()]))
            .pipe(rename({ extname: '.min.css' }))
            .pipe(gulp.dest(paths.dist));
    }));
});

gulp.task('bundle-libs', gulp.parallel('bundle-libs:js', 'bundle-libs:css'));

gulp.task('min:css', async () => {
    const plugins = [
        autoprefixer(),
        cssnano()
    ];

    return gulp.src(`${paths.scss}/main.scss`, {sourcemaps: true})
        .pipe(sass().on('error', sass.logError))
        .pipe(postcss(plugins))
        .pipe(inject.prepend('/* Created: ' + now + ' */\n'))
        .pipe(rename((path) => {
            path.basename = 'site.min'
        }))
        .pipe(gulp.dest(paths.dist));
});

gulp.task('min:js', async () => {
    return gulp.src(`${paths.js}/*.js`)
        .pipe(sourcemaps.init())
        .pipe(concat('site.js'))
        .pipe(inject.prepend('/* Created: ' + now + ' */\n'))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest(paths.dist))
        .pipe(terser())
        .pipe(rename({ extname: '.min.js' }))
        .pipe(gulp.dest(paths.dist));
});

gulp.task('clean', (cb) => {
    rimraf(paths.dist, cb);
});

gulp.task('minify-all', gulp.parallel('min:js', 'min:css'));

gulp.task('build', gulp.series('clean', 'bundle-libs', 'minify-all'));

gulp.task('watch', async () => {
    gulp.watch(`${paths.scss}/*.scss`, gulp.series('min:css'));
    gulp.watch(`${paths.js}/*.js`, gulp.series('bundle-libs:js', 'min:js'));
});

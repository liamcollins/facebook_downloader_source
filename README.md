# facebook_downloader_source
Source code repo for tool to download "photos of me" on facebook

Right now I'm having people download "production" files in a zip from here:
https://github.com/liamcollins/facebook-photo-downloader

Eventually these two repos should probably be merged.

In the meantime a few things that could be made better:

1. Obviously the "WindowsFormsApp1" name should go.
2. Using xpath the way I am currently is very fragile and will break as soon as facebook changes anything. 
  Xpaths could possibly be tweaked to be at least a little less fragile.
  Xpaths could be pulled live from somewhere so that they can be updated for those who have already downloaded the exe.
  Maybe there's a completely better way.
3. Deployment is a problem - it would be good to bundle everything in such a way that there is either an installer or just one exe file for download, not a whole zip file.
4. Maybe some kind of open licensing agreement?
5. Note about "as is" nature of software.
6. Not necessary but would be a cool addition to actually download videos too.
7. JIT debugging should be enabled. Also main window should close automatically when chrome is closed, not give ugly error.

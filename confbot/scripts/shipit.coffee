module.exports = (robot) ->
  robot.hear /\bship\s*it\b/i, (msg) ->
    msg.send "http://shipitsquirrel.github.io/images/ship%20it%20squirrel.png"

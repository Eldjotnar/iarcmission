class GroundRobot extends Phaser.Sprite {
  constructor(game, sf, angle, x, y, image) {
    super(game, x, y, image);
    this.anchor.setTo(0.5);

    this.count = 0;

    this.height = this.height/4 * sf;
    this.width = this.width/4 * sf;

    this.angle = angle;
    this.smoothed = false;

    this.game.physics.p2.enableBody(this);
    this.body.setCircle(10);
    this.body.fixedRotation = true;

    this.mySpeed = 50/3;//-(50/3)*sf;

    this.body.velocity.y = this.mySpeed*Math.sin(this.angle*Math.PI/180);
    this.body.velocity.x = this.mySpeed*Math.cos(this.angle*Math.PI/180);

    this.body.damping= 0;
    this.body.mass = 0.1;

    this.body.onBeginContact.add(this.robotHit, this);
    this.game.stage.addChild(this);

    this.game.time.events.repeat(Phaser.Timer.SECOND * 5, 30, this.spin, this);
  }
  update() {
    //this.body.setZeroVelocity();
    //this.body.moveLeft(20);
  //   //Math.abs(-4);
  //   if(Math.abs(this.x) > this.bounds.x) {
  //     console.log('out of bounds');
  //     this.destroy();
  //   }
  //   if(Math.abs(this.y) > this.bounds.y) {
  //     if(this.y < 0) {
  //       console.log('points scored');
  //     }
  //     this.destroy();
  //   }
  //   //console.log(this.x);
  }
  robotHit() {
    //console.log("collision!");
  }
  keepGoing() {
    this.body.angularVelocity = 0;
    this.angle = this.myAngle;
    this.body.fixedRotation = true;
    console.log(this.angle);

    this.body.velocity.y = this.mySpeed*Math.sin(this.angle*Math.PI/180);
    this.body.velocity.x = this.mySpeed*Math.cos(this.angle*Math.PI/180);
    // console.log("New: " + this.angle)
    // this.body.velocity = this.oldVelocity;
    // this.game.physics.arcade.velocityFromAngle(this.angle, 10, this.body.velocity);
    // console.log("x: " + this.x + " y: " + this.y);
  }
  spin() {
    // console.log('called');
    // console.log("before: " + this.myAngle)
    // this.myAngle = this.angle + Math.floor(Math.random() * 21);
    // console.log("after: " + this.myAngle);
    var angleChange = Math.floor(Math.random() * 21);
    if(this.count == 4) {
      // this.myAngle += 180;
      angleChange = 180;
      this.count = 0;
    } else {
      this.count++;
    }
    this.myAngle = this.angle + angleChange;
    // this.oldVelocity = this.body.velocity;
    this.body.setZeroVelocity();

    // for some godforsaken reason, the robot spins at -55.763767deg/sec
    this.body.fixedRotation = false;
    this.body.angularVelocity = (this.angle/this.angle)*2; // hell if I know what the units are
    var time = 1/(55.763767/angleChange);

    this.game.time.events.add(Phaser.Timer.SECOND * time, this.keepGoing, this);
  }
}

export default GroundRobot;

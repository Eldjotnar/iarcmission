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
  robotHit() {
    //console.log("collision!");
  }
  keepGoing() {
    this.body.angularVelocity = 0;
    this.angle = this.myAngle;
    this.body.fixedRotation = true;

    this.body.velocity.y = this.mySpeed*Math.sin(this.angle*Math.PI/180);
    this.body.velocity.x = this.mySpeed*Math.cos(this.angle*Math.PI/180);
  }
  spin() {
    var angleChange = Math.floor(Math.random() * 21);
    if(this.count == 4) {
      angleChange = 180;
      this.count = 0;
    } else {
      this.count++;
    }
    this.myAngle = this.angle + angleChange;
    this.body.setZeroVelocity();

    // for some godforsaken reason, the robot spins at -55.763767deg/sec
    this.body.fixedRotation = false;
    this.body.angularVelocity = 2; // hell if I know what the units are
    var time = 1/(55.763767/angleChange);

    this.game.time.events.add(Phaser.Timer.SECOND * time, this.keepGoing, this);
  }
}

export default GroundRobot;
